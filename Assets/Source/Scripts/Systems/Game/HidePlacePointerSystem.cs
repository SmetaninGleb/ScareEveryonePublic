

using UnityEngine;
using Kuhpik;

class HidePlacePointerSystem : GameSystem, IUpdating
{
    [SerializeField]
    private float _appearDistance = 7f;

    public void OnUpdate()
    {
        foreach (HidePlaceComponent hidePlace in game.HidePlaceList)
        {
            if (!hidePlace.IsDestroyed && !game.Player.HideableComponent.IsHiding
                && Vector3.Distance(hidePlace.transform.position, game.Player.transform.position) <= _appearDistance)
            {
                hidePlace.ShowPointer();
            }
            else
            {
                hidePlace.DisablePointer();
            }
        }
    }
}