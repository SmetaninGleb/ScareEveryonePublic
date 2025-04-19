
using UnityEngine;
using Kuhpik;

class HideableLoadingSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        game.HideableList = FindObjectsOfType<HideableComponent>();
        foreach (HideableComponent hideable in game.HideableList)
        {
            hideable.InitializeVariables();
        }
    }
}