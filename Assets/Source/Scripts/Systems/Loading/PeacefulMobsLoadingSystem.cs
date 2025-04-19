using UnityEngine;
using Kuhpik;
using UnityEngine.AI;

class PeacefulMobsLoadingSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        game.PeacefulMobList = FindObjectsOfType<PeacefulMobComponent>();
        foreach (PeacefulMobComponent mob in game.PeacefulMobList)
        {
            mob.InitializeVariables();
            mob.SetIsSoundOn(player.IsSoundOn);
        }
    }
}