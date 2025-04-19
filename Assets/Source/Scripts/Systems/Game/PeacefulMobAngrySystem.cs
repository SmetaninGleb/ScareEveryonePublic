
using UnityEngine;
using Kuhpik;

class PeacefulMobAngrySystem : GameSystem, IUpdating
{
    public void OnUpdate()
    {
        foreach (PeacefulMobComponent mob in game.PeacefulMobList)
        {
            mob.SetIsSoundOn(player.IsSoundOn);
            mob.SetIsVibroOn(player.IsVibroOn);
            if (mob.IsAngry && !mob.IsBecameAngry && !mob.IsScared)
            {
                HidePlaceComponent nearestHidePlace = null;
                float nearestHidePlaceDistance = float.PositiveInfinity;
                foreach (HidePlaceComponent hidePlace in game.HidePlaceList)
                {
                    if (hidePlace.ShouldBeDestroyed) continue;
                    if (nearestHidePlace == null)
                    {
                        nearestHidePlace = hidePlace;
                        nearestHidePlaceDistance = (mob.transform.position - hidePlace.transform.position).magnitude;
                        continue;
                    }
                    float hidePlaceDistance = (hidePlace.transform.position - mob.transform.position).magnitude;
                    if (hidePlaceDistance < nearestHidePlaceDistance)
                    {
                        nearestHidePlace = hidePlace;
                        nearestHidePlaceDistance = hidePlaceDistance;
                    }
                }
                nearestHidePlace.NavMeshObstacle.carving = false;
                game.NavMeshSurface.BuildNavMesh();
                mob.BecomeAngry(nearestHidePlace);
                mob.SetIsSoundOn(player.IsSoundOn);
            }
        }
    }
}