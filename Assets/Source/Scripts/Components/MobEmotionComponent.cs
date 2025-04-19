using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using DG.Tweening;

public class MobEmotionComponent : MonoBehaviour
{
    [SerializeField]
    private Image _emotionImage;
    [SerializeField]
    private Sprite _angrySprite;
    [SerializeField]
    private Sprite _questionSprite;
    [SerializeField]
    private Sprite _scarySprite;
    [SerializeField]
    private Sprite _passOutSprite;
    [SerializeField]
    private float _emotionDuration;
    private float _startEmotionTime;
    [Foldout("Animation")]
    [SerializeField]
    private float _appearDuration;
    [Foldout("Animation")]
    [SerializeField]
    private Vector3 _punch;

    public void InitializeValues()
    {
        _emotionImage.preserveAspect = true;
        _emotionImage.gameObject.SetActive(false);
        _startEmotionTime = -1f;
    }

    public void ShowAngryEmotion()
    {
        _emotionImage.gameObject.SetActive(true);
        _emotionImage.sprite = _angrySprite;
        _emotionImage.preserveAspect = true;
        _emotionImage.transform.DOPunchScale(_punch, _appearDuration);
        _startEmotionTime = Time.timeSinceLevelLoad;
    }

    public void ShowQuestionEmotion()
    {
        _emotionImage.gameObject.SetActive(true);
        _emotionImage.sprite = _questionSprite;
        _emotionImage.preserveAspect = true;
        _emotionImage.transform.DOPunchScale(_punch, _appearDuration);
        _startEmotionTime = Time.timeSinceLevelLoad;
    }

    public void ShowScaryEmotion()
    {
        _emotionImage.gameObject.SetActive(true);
        _emotionImage.sprite = _scarySprite;
        _emotionImage.preserveAspect = true;
        _emotionImage.transform.DOPunchScale(_punch, _appearDuration);
        _startEmotionTime = Time.timeSinceLevelLoad;
    }

    public void ShowPassOutEmotion()
    {
        _emotionImage.gameObject.SetActive(true);
        _emotionImage.sprite = _passOutSprite;
        _emotionImage.preserveAspect = true;
        _emotionImage.transform.DOPunchScale(_punch, _appearDuration);
        _startEmotionTime = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        Quaternion cameraRotation = Camera.main.transform.rotation;
        Vector3 oppositeCameraLookVector = Camera.main.transform.forward * -1f;
        Quaternion newRotation = Quaternion.LookRotation(oppositeCameraLookVector);
        transform.rotation = newRotation;
        if (_startEmotionTime == -1f) return;
        if (Time.timeSinceLevelLoad - _startEmotionTime >= _emotionDuration)
        {
            _emotionImage.gameObject.SetActive(false);
            _startEmotionTime = -1f;
        }
    }
}
