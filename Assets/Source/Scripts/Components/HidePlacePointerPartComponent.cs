using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HidePlacePointerPartComponent : MonoBehaviour
{
    [SerializeField]
    private string _animationBoolName;
    private Animator _animator;
    
    public void InitializeValues()
    {
        _animator = GetComponent<Animator>();
        Disable();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _animator.SetBool(_animationBoolName, true);
    }

    public void Disable()
    {
        _animator.SetBool(_animationBoolName, false);
        gameObject.SetActive(false);
    }
}
