

using Kuhpik;

class RandomMovementLoading : GameSystem, IIniting
{
    public void OnInit()
    {
        game.RandomMovementDestList = FindObjectsOfType<RandomMovementDestinationComponent>();
        game.RandomMovementList = FindObjectsOfType<RandomMovementComponent>();
        foreach (RandomMovementComponent movementComp in game.RandomMovementList)
        {
            movementComp.InitializeValues();
        }
    }
}