


using Kuhpik;

class RandomMovingControlSystem : GameSystem, IUpdating
{
    public void OnUpdate()
    {
        foreach (RandomMovementComponent moveComp in game.RandomMovementList)
        {
            moveComp.UpdateValues();
        }
    }
}