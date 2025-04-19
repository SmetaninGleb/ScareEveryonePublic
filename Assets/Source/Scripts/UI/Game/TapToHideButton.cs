using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class TapToHideButton : MonoBehaviour
{
    public event Action TapToHideEvent;

    [Foldout("Animations")]
    [SerializeField]
    private Vector3 _scalePunch;
    [Foldout("Animations")]
    [SerializeField]
    private float _scaleDuration;
    [Foldout("Animations")]
    [SerializeField]
    private Vector3 _rotationPunch;
    [Foldout("Animations")]
    [SerializeField]
    private float _rotationDuration;


    public void TapToHideButtonTapped()
    {
        TapToHideEvent?.Invoke();
    }

    public void Appear()
    {
        transform.DOPunchScale(_scalePunch, _scaleDuration);
        transform.DOPunchRotation(_rotationPunch, _rotationDuration, elasticity:0);
    }
}
