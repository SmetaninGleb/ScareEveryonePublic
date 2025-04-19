using Kuhpik;
using UnityEngine;


class PlayerLoadingSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        game.Player = FindObjectOfType<PlayerComponent>();
        game.Player.HideableComponent = game.Player.GetComponent<HideableComponent>();
        game.Player.InitializeVariables();
    }
}