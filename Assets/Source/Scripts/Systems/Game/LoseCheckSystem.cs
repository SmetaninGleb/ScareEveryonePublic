
using UnityEngine;
using Kuhpik;
using MoreMountains.NiceVibrations;

class LoseCheckSystem : GameSystem, IUpdating, IIniting
{
    public void OnInit()
    {
        game.StartGameTime = Time.timeSinceLevelLoad;
    }

    public void OnUpdate()
    {
        bool isLose = false;
        if (Time.timeSinceLevelLoad - game.StartGameTime >= game.TimeToLose)
        {
            isLose = true;
        }
        bool isAnyHidePlace = false;
        foreach (HidePlaceComponent hidePlace in game.HidePlaceList)
        {
            if (!hidePlace.IsDestroyed)
            {
                isAnyHidePlace = true;
                break;
            }
        }
        if (!isAnyHidePlace)
        {
            isLose = true;
        }
        if (isLose)
        {
            foreach (PeacefulMobComponent mob in game.PeacefulMobList)
            {
                if (mob.IsScared) continue;
                mob.AnimComponent.StopWalkAnim();
                mob.NavMeshAgent.SetDestination(mob.transform.position);
                mob.NavMeshAgent.isStopped = true;
            }
            if (player.IsVibroOn)
            {
                MMVibrationManager.Haptic(HapticTypes.Failure);
            }
            Bootstrap.ChangeGameState(EGamestate.Lose);
        }
    }
}