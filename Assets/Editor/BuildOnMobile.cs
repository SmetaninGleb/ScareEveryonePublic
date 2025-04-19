using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

#if UNITY_FACEBOOK
using Facebook.Unity;
using Facebook.Unity.Settings;
#endif

#if UNITY_GA
using GameAnalyticsSDK;
#endif

namespace DorferGames.Editor
{

    public static class BuildOnMobile
    {
        static readonly string Eol = Environment.NewLine;

        static readonly string keystoreName = "dorfer.keystore";
        static readonly string keystorePass = "4afW7SwZjsN&kKtXe7KwS%Ye";
        static readonly string keyaliasName = "release";
        static readonly string keyaliasPass = "4afW7SwZjsN&kKtXe7KwS%Ye";

        static readonly string pinKey = "PIN";
        static readonly int GA_key_index = 0;

        [UsedImplicitly]
        public static void BuildOptions()
        {
            Dictionary<string, string> options = GetValidatedOptions();
            var buildTarget = (BuildTarget)Enum.Parse(typeof(BuildTarget), options["buildTarget"]);

            PlayerSettings.Android.bundleVersionCode = int.Parse(options["androidVersionCode"]);
            EditorUserBuildSettings.buildAppBundle = options["customBuildPath"].EndsWith(".aab");

            PlayerSettings.Android.keystoreName = keystoreName;
            PlayerSettings.Android.keystorePass = keystorePass;
            PlayerSettings.Android.keyaliasName = keyaliasName;
            PlayerSettings.Android.keyaliasPass = keyaliasPass;

            // So you don't have to switch between MONO and IL2CPP everytime.
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.All;

            PlayerSettings.SplashScreen.showUnityLogo = false;
            Build(buildTarget, options["customBuildPath"]);
        }

        private static Dictionary<string, string> GetValidatedOptions()
        {
            ParseCommandLineArguments(out Dictionary<string, string> validatedOptions);

            if (!validatedOptions.TryGetValue("projectPath", out string _))
            {
                Console.WriteLine("Missing argument -projectPath");
                EditorApplication.Exit(110);
            }

            if (!validatedOptions.TryGetValue("buildTarget", out string buildTarget))
            {
                Console.WriteLine("Missing argument -buildTarget");
                EditorApplication.Exit(120);
            }

            if (!Enum.IsDefined(typeof(BuildTarget), buildTarget ?? string.Empty))
            {
                EditorApplication.Exit(121);
            }

            if (validatedOptions.TryGetValue("buildPath", out string buildPath))
            {
                validatedOptions["customBuildPath"] = buildPath;
            }

            if (!validatedOptions.TryGetValue("customBuildPath", out string _))
            {
                Console.WriteLine("Missing argument -customBuildPath");
                EditorApplication.Exit(130);
            }

            return validatedOptions;
        }

        private static void ParseCommandLineArguments(out Dictionary<string, string> providedArguments)
        {
            providedArguments = new Dictionary<string, string>();
            string[] args = Environment.GetCommandLineArgs();

            Console.WriteLine(
                $"{Eol}" +
                $"###########################{Eol}" +
                $"#    Parsing settings     #{Eol}" +
                $"###########################{Eol}" +
                $"{Eol}"
            );

            // Extract flags with optional values
            for (int current = 0, next = 1; current < args.Length; current++, next++)
            {
                // Parse flag
                bool isFlag = args[current].StartsWith("-");
                if (!isFlag) continue;
                string flag = args[current].TrimStart('-');

                // Parse optional value
                bool flagHasValue = next < args.Length && !args[next].StartsWith("-");
                string value = flagHasValue ? args[next].TrimStart('-') : "";
                string displayValue = "\"" + value + "\"";

                // Assign
                Console.WriteLine($"Found flag \"{flag}\" with value {displayValue}.");
                providedArguments.Add(flag, value);
            }

            CheckSDK(providedArguments[pinKey]);
        }

        private static void Build(BuildTarget buildTarget, string filePath)
        {
            string[] scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(s => s.path).ToArray();

            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = scenes,
                target = buildTarget,
                locationPathName = filePath,
            };

            BuildSummary buildSummary = BuildPipeline.BuildPlayer(buildPlayerOptions).summary;
            ReportSummary(buildSummary);
            ExitWithResult(buildSummary.result);
        }

        private static void ReportSummary(BuildSummary summary)
        {
            Console.WriteLine(
                $"{Eol}" +
                $"###########################{Eol}" +
                $"#      Build results      #{Eol}" +
                $"###########################{Eol}" +
                $"{Eol}" +
                $"Duration: {summary.totalTime.ToString()}{Eol}" +
                $"Warnings: {summary.totalWarnings.ToString()}{Eol}" +
                $"Errors: {summary.totalErrors.ToString()}{Eol}" +
                $"Size: {summary.totalSize.ToString()} bytes{Eol}" +
                $"Logo: {PlayerSettings.SplashScreen.showUnityLogo.ToString()}{Eol}" +
                $"{Eol}"
            );

            Process.Start("/bin/bash", $"-c \"echo ::set-output name=unityVersion::{Application.unityVersion}\"");
            Process.Start("/bin/bash", $"-c \"echo ::set-output name=productNameSet::{Application.productName}\"");
            Process.Start("/bin/bash", $"-c \"echo ::set-output name=productBundleSet::{Application.identifier}\"");
            Process.Start("/bin/bash", $"-c \"echo ::set-output name=productVersionSet::{Application.version}\"");
            Process.Start("/bin/bash", $"-c \"echo ::set-output name=productVersionCodeSet::{PlayerSettings.Android.bundleVersionCode}\"");
        }

        private static void CheckSDK(string pin)
        {
            var clean = pin.Trim('\"');
            var builder = new StringBuilder();
            var infoBuilder = new StringBuilder();
            var lines = clean.Split(new string[] { "NEWLINE" }, StringSplitOptions.RemoveEmptyEntries);

            builder.Append("\'"); //To make single string in bash
            builder.Append("*Проверка правильности ключей в SDK...*");

            infoBuilder.Append("\'");
            infoBuilder.Append("*Информация о ключах в проекте...*");

            for (int i = 1; i < lines.Length; i++) //Skipping 1st line (SDK's)
            {
                var pieces = lines[i].Trim().Split(':');
                var place = pieces[0];
                var value = pieces[1].Trim();

                Console.WriteLine($"Trying read SDK value {place} with key: {value}");

                Check(place, value, builder, infoBuilder);
            }

            builder.Append("\'"); //To make single string in bash
            infoBuilder.Append("\'");

            PrepareForExport(builder);
            PrepareForExport(infoBuilder);

            Console.WriteLine(builder.ToString());
            Console.WriteLine(infoBuilder.ToString());

            Process.Start("/bin/bash", $"-c \"echo ::set-output name=sdks::{builder.ToString()}\"");
            Process.Start("/bin/bash", $"-c \"echo ::set-output name=sdkinfo::{infoBuilder.ToString()}\"");
        }

        private static void Check(string place, string value, StringBuilder builder, StringBuilder infoBuilder)
        {
            if (place == "FB") CheckFB(value, builder, infoBuilder);
            if (place == "GA") CheckGA(value, builder, infoBuilder);
            if (place == "GA_SECRET") CheckGASecret(value, builder, infoBuilder);
        }

        private static void ExitWithResult(BuildResult result)
        {
            switch (result)
            {
                case BuildResult.Succeeded:
                    Console.WriteLine("Build succeeded!");
                    EditorApplication.Exit(0);
                    break;
                case BuildResult.Failed:
                    Console.WriteLine("Build failed!");
                    EditorApplication.Exit(101);
                    break;
                case BuildResult.Cancelled:
                    Console.WriteLine("Build cancelled!");
                    EditorApplication.Exit(102);
                    break;
                case BuildResult.Unknown:
                default:
                    Console.WriteLine("Build result is unknown!");
                    EditorApplication.Exit(103);
                    break;
            }
        }

        private static void CheckFB(string value, StringBuilder builder, StringBuilder infoBuilder)
        {
            var name = "Facebook";
#if UNITY_FACEBOOK
            AppendBuilder(name, FacebookSettings.AppId.ToString() == value, builder);
            AppendInfoBuilder(name, FacebookSettings.AppId.ToString(), value, infoBuilder);
            Console.WriteLine($"FB value {FacebookSettings.AppId} and pin value {value}");
#else
            AppendBuilderWithWarning(name, builder);
#endif
        }

        private static void CheckGA(string value, StringBuilder builder, StringBuilder infoBuilder)
        {
            var name = "Game Analytics";
#if UNITY_GA
            AppendBuilder(name, GameAnalytics.SettingsGA.GetGameKey(GA_key_index) == value, builder);
            AppendInfoBuilder(name, GameAnalytics.SettingsGA.GetGameKey(GA_key_index), value, infoBuilder);
#else
            AppendBuilderWithWarning(name, builder);
#endif
        }

        private static void CheckGASecret(string value, StringBuilder builder, StringBuilder infoBuilder)
        {
            var name = "Game Analytics Secret";
#if UNITY_GA
            AppendBuilder(name, GameAnalytics.SettingsGA.GetSecretKey(GA_key_index) == value, builder);
            AppendInfoBuilder(name, GameAnalytics.SettingsGA.GetSecretKey(GA_key_index), value, infoBuilder);
#else
            AppendBuilderWithWarning(name, builder);
#endif
        }

        /// <summary>
        /// Appends line with value "ok" or "wrong"
        /// </summary>
        private static void AppendBuilder(string name, bool isValueSame, StringBuilder builder)
        {
            builder.Append(Eol);
            builder.Append($"{name}: { (isValueSame ? ":ok:" : ":x:") }");
            Console.WriteLine($"{name} is {isValueSame}");
        }

        /// <summary>
        /// If no integration found
        /// </summary>
        private static void AppendBuilderWithWarning(string name, StringBuilder builder)
        {
            builder.Append(Eol);
            builder.Append($"Интеграция *{name}* не была найдена.");
            Console.WriteLine($"{name} was not found");
        }

        private static void AppendInfoBuilder(string name, string value, string expected, StringBuilder infoBuilder)
        {
            infoBuilder.Append(Eol);
            infoBuilder.Append($"Ключ *{name}* в проекте: {value}. Ожидалось {expected}");
        }

        private static void PrepareForExport(StringBuilder builder)
        {
            builder.Replace("%", "%25");
            builder.Replace("\n", "%0A");
            builder.Replace("\r", "%0D");
        }
    }
}
