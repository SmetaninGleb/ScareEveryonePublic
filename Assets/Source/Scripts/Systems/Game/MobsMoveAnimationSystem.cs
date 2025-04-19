

using Kuhpik;

class MobsMoveAnimationSystem : GameSystem, IUpdating
{
    public void OnUpdate()
    {
        foreach (PeacefulMobComponent mob in game.PeacefulMobList)
        {
            if (mob.RandomMovementComponent.IsMoving || mob.IsGoToDestroy)
            {
                mob.AnimComponent.StartWalkAnim();
            } else if (mob.RandomMovementComponent.IsWaiting)
            {
                mob.AnimComponent.StopWalkAnim();
            }
        }
    }
}