using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerAnimComponent))]
[RequireComponent(typeof(HideableComponent))]
public class PlayerComponent : MonoBehaviour
{
    [SerializeField]
    private float _maxSpeed;

    [HideInInspector]
    public HideableComponent HideableComponent;
    [HideInInspector]
    public NavMeshAgent NavMeshAgent;
    [HideInInspector]
    public PlayerAnimComponent AnimComponent;
    [HideInInspector]
    public bool IsMoving;

    public float MaxSpeed => _maxSpeed;

    public void InitializeVariables()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.speed = _maxSpeed;
        AnimComponent = GetComponent<PlayerAnimComponent>();
        IsMoving = false;
    }
}
