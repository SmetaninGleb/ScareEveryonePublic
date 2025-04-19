
using UnityEngine;
using Kuhpik;

class JoystickLoadingSystem : GameSystem, IIniting
{
    [SerializeField]
    private DynamicJoystick joystick;
    public void OnInit()
    {
        game.Joystick = joystick;
        game.JoystickSiblingIndex = joystick.transform.GetSiblingIndex();
    }
}