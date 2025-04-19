using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerMovementSystem : GameSystem, IUpdating
{
    [Required]
    public GameObject JoystickPrefab;
    [Required]
    public Transform JoystickScreen;

    public void OnUpdate()
    {
        if (!game.Player.gameObject.activeSelf) return;
        if (game.ShouldReloadJoystick)
        {
            Destroy(game.Joystick.gameObject);
            game.Joystick = Instantiate(JoystickPrefab, JoystickScreen).GetComponent<DynamicJoystick>();
            game.Joystick.transform.SetSiblingIndex(game.JoystickSiblingIndex);
            game.ShouldReloadJoystick = false;
        }
        Vector3 rotationVector = new Vector3();
        rotationVector.x = game.Joystick.Direction.x;
        rotationVector.z = game.Joystick.Direction.y;
        if (rotationVector != Vector3.zero)
        {
            game.Player.transform.rotation = Quaternion.LookRotation(rotationVector);
        }
        float joystickMoveLength = Mathf.Sqrt(Mathf.Pow(game.Joystick.Horizontal, 2) + Mathf.Pow(game.Joystick.Vertical, 2));
        float moveDistance = Mathf.Lerp(0, game.Player.MaxSpeed, joystickMoveLength);
        if (moveDistance == 0)
        {
            game.Player.IsMoving = false;
            return;
        }
        game.Player.IsMoving = true;
        game.Player.NavMeshAgent.Move(moveDistance * rotationVector.normalized * Time.deltaTime);
    }
}
