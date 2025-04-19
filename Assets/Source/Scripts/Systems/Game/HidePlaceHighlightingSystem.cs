
using UnityEngine;
using Kuhpik;

class HidePlaceHighlightingSystem : GameSystem, IUpdating
{
    public void OnUpdate()
    {
        foreach (HidePlaceComponent hidePlace in game.HidePlaceList)
        {
            if (hidePlace.IsHighlighted)
            {
                hidePlace.HighlightWithMatrial(config.HighlightingMaterial);
            }
            else
            {
                hidePlace.StopHighlighting();
            }
        }
    }
}