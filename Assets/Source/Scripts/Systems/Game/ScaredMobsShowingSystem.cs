

using Kuhpik;
using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class ScaredMobsShowingSystem : GameSystem, IUpdating, IIniting
{
    [Required]
    [SerializeField]
    private TMP_Text ScaredMobsText;
    [Required]
    [SerializeField]
    private MobsShowingScreen _mobsShowingScreen;

    public void OnInit()
    {
        _mobsShowingScreen.DrawImgs(config.LevelContainer.Levels[player.CurrentLevelIndex].MobsToScareForWin);
    }

    public void OnUpdate()
    {
        _mobsShowingScreen.UpdateDeadNumber(game.ScaredMobs);
        //string textToShow = string.Format("{0}/{1}", game.ScaredMobs, config.LevelContainer.Levels[player.CurrentLevelIndex].MobsToScareForWin);
        //ScaredMobsText.text = textToShow;
    }
}