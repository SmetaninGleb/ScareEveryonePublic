

using Kuhpik;

class PlayerAnimationLoadingSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        game.Player.AnimComponent.InitializeVariables();
    }
}