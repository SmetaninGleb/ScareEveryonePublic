

using Kuhpik;
using UnityEngine;

class SliderShowingSystem : GameSystem, IUpdating
{
    public void OnUpdate()
    {
        if (!game.Player.HideableComponent.IsHiding) return;
        bool isMobNearPlayer = false;
        foreach (PeacefulMobComponent mob in game.PeacefulMobList)
        {
            if (game.Player.HideableComponent.CanScare(mob))
            {
                isMobNearPlayer = true;
                break;
            }
        }
        if (isMobNearPlayer)
        {
            if (!game.ScareScreen.ScareSlider.gameObject.activeSelf)
            {
                game.ScareScreen.ScareSlider.gameObject.SetActive(true);
                game.ScareScreen.ScareSlider.StartSliderMove();
            }
        } else
        {
            game.ScareScreen.ScareSlider.transform.localScale = new Vector3(1f, 1f, 1f);
            game.ScareScreen.ScareSlider.gameObject.SetActive(false);
            game.ScareScreen.ScareSlider.StopSliderMove();
        }
    }
}