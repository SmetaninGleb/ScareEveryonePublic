using UnityEngine;
using UnityEngine.AI;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    public class GameData
    {
        // Example (I use public fields for data, but u free to use properties\methods etc)
        // public float LevelProgress;
        // public Enemy[] Enemies;
        public PlayerComponent Player;
        public DynamicJoystick Joystick;
        public int JoystickSiblingIndex;
        public bool ShouldReloadJoystick = false;

        public PeacefulMobComponent[] PeacefulMobList;
        public RandomMovementComponent[] RandomMovementList;
        public RandomMovementDestinationComponent[] RandomMovementDestList;

        public HidePlaceComponent[] HidePlaceList;
        public HideableComponent[] HideableList;
        public TapToHideButton TapToHideButton;
        public ScareScreen ScareScreen;

        public float StartGameTime;
        public float TimeToLose;
        public int ScaredMobs = 0;
        public int RandomDestBlockedNumber = 0;

        public NavMeshSurface NavMeshSurface;
    }
}