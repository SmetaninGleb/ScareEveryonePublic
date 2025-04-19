using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimComponent : MonoBehaviour
{
    private Animator Animator;
    [SerializeField]
    private string _walkAnimName;
    
    public void InitializeVariables()
    {
        Animator = GetComponent<Animator>();
    }

    public void StartWalkAnimation()
    {
        Animator.SetBool(_walkAnimName, true);
    }

    public void StopWalkAnimation()
    {
        Animator.SetBool(_walkAnimName, false);
    }
}
