using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using GameAnalyticsSDK;

public class LevelLoadingSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        GameObject level = Instantiate(config.LevelContainer.Levels[player.CurrentLevelIndex].gameObject);
        game.TimeToLose = level.GetComponent<LevelComponent>().TimeToLose;
        game.ScaredMobs = 0;
        
        GameAnalytics.NewDesignEvent("level start");
        if (!player.IsLevelRandom)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, player.CurrentLevelIndex.ToString());
        }
        else
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Random Level. All levels are complited.");
        }
    }

    private void NonCallabeMethodForInitializingVibrationForPermission()
    {
        Handheld.Vibrate();
        MMVibrationManager.StopAllHaptics();
    }
}
