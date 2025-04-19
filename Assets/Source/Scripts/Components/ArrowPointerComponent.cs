using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using DG.Tweening;

public class ArrowPointerComponent : MonoBehaviour
{
    [SerializeField]
    private Image _arrow;
    [MinMaxSlider(-5f, 5f)]
    [SerializeField]
    private Vector2 _arrowYMovingRange = new Vector2(-1f, 1f);
    [SerializeField]
    private float _arrowSpeedMultiplier = 3f;
    private bool _isArrowMoving;
    
    public void StartArrowMoving()
    {
        _arrow.color = Color.white;
        _isArrowMoving = true;
    }

    private void Update()
    {
        Vector3 oppositeCameraDirection = (transform.position - Camera.main.transform.position).normalized * -1f;
        Vector3 newRotationEuler = Quaternion.LookRotation(oppositeCameraDirection).eulerAngles;
        newRotationEuler.x = 0f;
        newRotationEuler.z = 0f;
        transform.rotation = Quaternion.Euler(newRotationEuler);
        if (_isArrowMoving)
        {
            float newCoorMult = (Mathf.Sin(Time.timeSinceLevelLoad * _arrowSpeedMultiplier) + 1) / 2;
            float newYCoor = newCoorMult * (_arrowYMovingRange.y - _arrowYMovingRange.x) + _arrowYMovingRange.x;
            _arrow.transform.localPosition = new Vector3(_arrow.transform.localPosition.x, newYCoor, _arrow.transform.localPosition.z);
        }
    }
}
