

using Kuhpik;
using UnityEngine;

class RandomMovingCorrectnessCheckingSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        int movingNum = 0;
        foreach(RandomMovementComponent movable in game.RandomMovementList)
        {
            if (!movable.DoNotMove) movingNum++;
        }
        if (movingNum >= game.RandomMovementDestList.Length)
        {
            Debug.LogError("Number of destinations is too small. Number of destinatoins should be greater than number of monsters.");
            Debug.Break();
        }
    }
}