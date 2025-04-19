

using Kuhpik;
using UnityEngine;

class RandomMovementSystem : GameSystem, IUpdating
{
    public void OnUpdate()
    {
        foreach (RandomMovementComponent moveComponent in game.RandomMovementList)
        {
            if (moveComponent.DoNotMove) continue;
            if (moveComponent.IsMovingBlocked) continue;
            if (moveComponent.IsMoving || moveComponent.IsWaiting) continue;
            if (moveComponent.IsReadyToMove)
            {
                RandomMovementDestinationComponent newDestination;
                int randomDestinationIndex = Random.Range(0, game.RandomMovementDestList.Length);
                int startRandomIndex = randomDestinationIndex;
                while (game.RandomMovementDestList[randomDestinationIndex].IsOccupied 
                    || game.RandomMovementDestList[randomDestinationIndex].IsBlocked)
                {
                    randomDestinationIndex = (randomDestinationIndex + 1) % game.RandomMovementDestList.Length;
                    if (randomDestinationIndex == startRandomIndex)
                    {
                        Debug.LogError("No random destination for " + moveComponent.gameObject.name);
                        Application.Quit();
                    }
                }
                newDestination = game.RandomMovementDestList[randomDestinationIndex];
                moveComponent.StartMoving(newDestination);
            } else if (moveComponent.IsReadyToWait)
            {
                Vector2 waitingTimeRange = moveComponent.CurrentDestination.WaitingTimeRange;
                float timeToWait = Random.Range(waitingTimeRange.x, waitingTimeRange.y);
                moveComponent.StartWaiting(timeToWait);
            }
        }
    }
}