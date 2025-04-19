using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobsVisionSystem : GameSystem, IFixedUpdating
{
    [SerializeField]
    private int _randomPointPassesNumber = 10;

    public void OnFixedUpdate()
    {
        foreach (PeacefulMobComponent mob in game.PeacefulMobList)
        {
            if (mob.IsScared || game.Player.HideableComponent.IsHiding || mob.IsRunning || mob.IsAngry) continue;
            if (mob.VisabilityComponent.CanSee(game.Player.transform))
            {
                Vector3 destination = GetRunDestinationPosition(mob);
                mob.StartRunning(destination);
            }
            else if (mob.VisabilityComponent.CanFeel(game.Player.transform))
            {
                Vector3 destination = GetRunDestinationPosition(mob);
                mob.StartRunning(destination);
            }
        }
    }

    private Vector3 GetRunDestinationPosition(PeacefulMobComponent mob)
    {
        Vector2 random2DDir = Random.insideUnitCircle;
        Vector3 random3DDir = new Vector3(random2DDir.x, 0f, random2DDir.y);
        Vector3 randomPos = mob.transform.position + random3DDir * mob.MaxRunDistance;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPos, out navHit, 1000f, NavMesh.AllAreas);
        float maxDistance = Vector3.Distance(navHit.position, mob.transform.position);
        Vector3 ansPosition = navHit.position;
        for (int i = 1; i < _randomPointPassesNumber; i++)
        {
            random2DDir = Random.insideUnitCircle;
            random3DDir = new Vector3(random2DDir.x, 0f, random2DDir.y);
            randomPos = mob.transform.position + random3DDir * mob.MaxRunDistance;
            NavMesh.SamplePosition(randomPos, out navHit, 1000f, NavMesh.AllAreas);
            float curDistance = Vector3.Distance(navHit.position, mob.transform.position);
            if (curDistance > maxDistance)
            {
                maxDistance = curDistance;
                ansPosition = navHit.position;
            }
        }
        return ansPosition;
    }
}
