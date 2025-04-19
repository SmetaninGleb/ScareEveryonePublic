

using UnityEngine;
using Kuhpik;
using MoreMountains.NiceVibrations;

class WinCheckingSystem : GameSystem, IUpdating
{
    public void OnUpdate()
    {
        if (!player.WasTutorial) return;
        if (game.Player.HideableComponent.IsHiding) return;
        if (game.ScaredMobs >= config.LevelContainer.Levels[player.CurrentLevelIndex].MobsToScareForWin)
        {
            if (player.IsVibroOn)
            {
                MMVibrationManager.Haptic(HapticTypes.Success);
            }
            player.WasTutorial = true;
            Bootstrap.ChangeGameState(EGamestate.Win);
        }
    }
}