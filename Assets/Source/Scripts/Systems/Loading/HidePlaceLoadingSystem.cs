
using UnityEngine;
using Kuhpik;

class HidePlaceLoadingSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        game.HidePlaceList = FindObjectsOfType<HidePlaceComponent>();
        foreach (HidePlaceComponent hidePlace in game.HidePlaceList)
        {
            hidePlace.InitializeVariables();
            hidePlace.SetVibro(player.IsVibroOn);
        }
    }
}