
using UnityEngine;
using Kuhpik;
using DG.Tweening;

class PlayerHideSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        game.Player.HideableComponent.TouchedEvent += PlayerTouchedHidePlace;
        game.Player.HideableComponent.UntouchedEvent += PlayerUntouchedHidePlace;
        game.TapToHideButton.TapToHideEvent += HidePlayer;
    }
    
    public void HidePlayer()
    {
        game.Player.HideableComponent.IsHiding = true;
        game.Player.HideableComponent.HidingPlace = game.Player.HideableComponent.TouchingHidePlace;
        game.Player.HideableComponent.PoofParticles.transform.SetParent(null);
        game.Player.HideableComponent.PoofParticles.transform.position = game.Player.transform.position;
        game.Player.HideableComponent.PoofParticles.Play();
        game.Player.HideableComponent.Hide();
        game.ScareScreen.gameObject.SetActive(true);
        game.ScareScreen.ScareSlider.gameObject.SetActive(false);
        game.TapToHideButton.gameObject.SetActive(false);
        game.Joystick.gameObject.SetActive(false);
        game.ShouldReloadJoystick = true;
        game.JoystickSiblingIndex = game.Joystick.transform.GetSiblingIndex();
        //RadiusSprite 
        Vector3 HidingPlacePosition = game.Player.HideableComponent.HidingPlace.transform.position;
        GameObject RadiusDraw = game.Player.HideableComponent.RadiusDraw.gameObject;
        RadiusDraw.transform.SetParent(null);
        RadiusDraw.SetActive(true);
        RadiusDraw.transform.position = new Vector3(HidingPlacePosition.x, RadiusDraw.transform.position.y, HidingPlacePosition.z);
    }

    public void PlayerTouchedHidePlace(HideableComponent hideable, HidePlaceComponent hidePlace)
    {
        if (!hideable.TryGetComponent(out PlayerComponent player) || hidePlace.IsDestroyed) return;
        bool isAnyoneAngry = false;
        foreach (PeacefulMobComponent mob in game.PeacefulMobList)
        {
            if (mob.IsAngry)
            {
                isAnyoneAngry = true;
                break;
            }
        }
        if (isAnyoneAngry) return;
        hidePlace.IsHighlighted = true;
        HidePlayer();
        //game.TapToHideButton.gameObject.SetActive(true);
        //game.TapToHideButton.Appear();
    }
    
    public void PlayerUntouchedHidePlace(HideableComponent hideable, HidePlaceComponent hidePlace)
    {
        if (!hideable.TryGetComponent(out PlayerComponent player)) return;
        hidePlace.IsHighlighted = false;
        game.TapToHideButton.gameObject.SetActive(false);
    }
}