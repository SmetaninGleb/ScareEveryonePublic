

using Kuhpik;
using UnityEngine;
using MoreMountains.NiceVibrations;

class PlayerScareSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        game.ScareScreen.ScareButton.onClick.AddListener(PlayerScare);
    }

    public void PlayerScare()
    {
        game.ScareScreen.gameObject.SetActive(false);
        bool IsSliderInGreenZone = game.ScareScreen.ScareSlider.IsInGreenZone();
        game.ScareScreen.ScareSlider.StopSliderMove();
        bool canScareAnybody = false;
        foreach (PeacefulMobComponent mob in game.PeacefulMobList)
        {
            if (game.Player.HideableComponent.CanScare(mob))
            {
                canScareAnybody = true;
                if (IsSliderInGreenZone)
                {
                    mob.BecomeScared();
                    game.ScaredMobs++;
                    game.Player.HideableComponent.HidingPlace.SetIsSoundOn(player.IsSoundOn);
                    game.Player.HideableComponent.HidingPlace.StartScare();
                    game.Player.HideableComponent.HidingPlace.AddScaredMob(mob);
                    game.Player.HideableComponent.HidingPlace.SetScaryingMonster(game.Player.HideableComponent);
                }
                else
                {
                    mob.IsAngry = true;
                    game.Player.HideableComponent.IsAppearing = true;
                }
            }
        }
        if (!canScareAnybody)
        {
            game.Player.HideableComponent.IsAppearing = true;
        }
        else
        {
            if (player.IsVibroOn)
            {
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            }
        }
    }
}