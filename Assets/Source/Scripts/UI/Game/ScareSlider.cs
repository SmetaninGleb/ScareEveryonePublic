using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScareSlider : MonoBehaviour
{
    [Required]
    [SerializeField]
    private RectTransform _sliderRT;
    [Required]
    [SerializeField]
    private RectTransform _greenBackground;
    [Required]
    [SerializeField]
    private RectTransform _redBackground;
    [SerializeField]
    private float _sliderSpeed;
    private bool _isMoving;
    private Slider _slider;
    private float _startTime;

    [Range(0f, 1f)]
    [SerializeField]
    private float _greenRedSlider;

    //Animation constants
    [Foldout("Animations")]
    [SerializeField]
    [Min(0.1f)]
    private float _appearDuration;
    [Foldout("Animations")]
    [SerializeField]
    private Vector3 _appearScale;
    [Foldout("Animations")]
    [SerializeField]
    [Min(1)]
    private int _appearVibrations;

    public void StartSliderMove()
    {
        _isMoving = true;
        _startTime = Time.timeSinceLevelLoad;
        DoStartSliderAnimation();
    }

    private void DoStartSliderAnimation()
    {
        _sliderRT.DOPunchScale(_appearScale, _appearDuration, _appearVibrations);
    }

    public void StopSliderMove()
    {
        _isMoving = false;
        _slider.value = 0f;
    }

    public bool IsInGreenZone()
    {
        return _slider.value >= _greenRedSlider;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _slider.value = (Mathf.Sin((Time.timeSinceLevelLoad - _startTime) * _sliderSpeed - Mathf.PI / 2f) + 1f) / 2f;
        }
    }

    public void InitializeValues()
    {
        _isMoving = false;
        _slider = _sliderRT.GetComponent<Slider>();
        _slider.value = 0f;
    }

    private void OnValidate()
    {
        _redBackground.offsetMin = new Vector2(0f, _redBackground.offsetMin.y);
        _greenBackground.offsetMax = new Vector2(0f, _greenBackground.offsetMax.y);
        _redBackground.offsetMax = new Vector2((_sliderRT.rect.xMax * _greenRedSlider - _sliderRT.rect.xMax) * 2, _redBackground.offsetMax.y);
        _greenBackground.offsetMin = new Vector2(_sliderRT.rect.xMax * _greenRedSlider * 2, _greenBackground.offsetMin.y);
    }
}
