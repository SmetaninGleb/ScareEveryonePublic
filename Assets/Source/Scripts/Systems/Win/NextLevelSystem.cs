

using GameAnalyticsSDK;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

class NextLevelSystem : GameSystem, IIniting
{
    [Required]
    public NextLevelButton NextLevelButton;

    public void OnInit()
    {
        NextLevelButton.NextLevelButtonEvent += NextLevelButtonPressed;

        if (!player.IsLevelRandom)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, player.CurrentLevelIndex.ToString());
        } else
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Random Level. All levels are complited.");
        }
    }

    public void NextLevelButtonPressed()
    {
        player.CurrentLevelIndex = GetNextLevel();
        Bootstrap.GameRestart(0);
    }
    
    private int GetNextLevel()
    {
        if (player.CurrentLevelIndex == config.LevelContainer.Levels.Length - 1)
        {
            player.IsLevelRandom = true;
        }
        if (player.IsLevelRandom)
        {
            int nextLevelIndex = Random.Range(0, config.LevelContainer.Levels.Length);
            LevelComponent nextLevel = config.LevelContainer.Levels[nextLevelIndex];
            while (nextLevel.IsTutorial || nextLevelIndex == player.CurrentLevelIndex)
            {
                nextLevelIndex++;
                nextLevel = config.LevelContainer.Levels[nextLevelIndex];
            }
            return nextLevelIndex;
        }
        else
        {
            return player.CurrentLevelIndex + 1;
        }
    }
}