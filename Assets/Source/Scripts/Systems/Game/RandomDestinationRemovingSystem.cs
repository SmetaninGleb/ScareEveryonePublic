

using Kuhpik;
using UnityEngine;

class RandomDestinationRemovingSystem : GameSystem, IUpdating
{
    public void OnUpdate()
    {
        int availableDestNum = 0;
        foreach (RandomMovementDestinationComponent dest in game.RandomMovementDestList)
        {
            if (!dest.IsBlocked)
            {
                availableDestNum++;
            }
        }
        int passedOutMobsNum = 0;
        foreach (PeacefulMobComponent mob in game.PeacefulMobList)
        {
            if (mob.IsPassedOut)
            {
                passedOutMobsNum++;
            }
        }
        if (availableDestNum - 1 <= game.PeacefulMobList.Length - passedOutMobsNum)
        {
            return;
        }
        foreach (HidePlaceComponent hidePlace in game.HidePlaceList)
        {
            if (availableDestNum - 1 <= game.PeacefulMobList.Length - passedOutMobsNum)
            {
                return;
            }
            if (hidePlace.IsDestroyed && hidePlace.CanRemoveNearDestination())
            {
                hidePlace.RemoveNearRandomDestination();
                game.RandomDestBlockedNumber++;
                availableDestNum--;
            }
        }
    }
}