
using UnityEngine;
using Kuhpik;

class MobsAnimationLoadingSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        foreach (PeacefulMobAnimComponent animComp in FindObjectsOfType<PeacefulMobAnimComponent>())
        {
            animComp.InitializeVariables();
        }
    }
}