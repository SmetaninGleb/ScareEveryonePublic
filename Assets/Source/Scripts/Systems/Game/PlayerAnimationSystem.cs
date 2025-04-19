

using Kuhpik;

class PlayerAnimationSystem : GameSystem, IUpdating
{
    public void OnUpdate()
    {
        if (game.Player.IsMoving)
        {
            game.Player.AnimComponent.StartWalkAnimation();
        } else
        {
            game.Player.AnimComponent.StopWalkAnimation();
        }
    }
}