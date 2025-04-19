
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using DG.Tweening;
using TMPro;
using System;

[RequireComponent(typeof(TMP_Text))]
public class TutorialText : MonoBehaviour
{
    [Foldout("Animation")]
    [SerializeField]
    private float _appearDuration;
    [Foldout("Animation")]
    [SerializeField]
    private Vector3 _appearPunch;
    [Foldout("Animation")]
    [SerializeField]
    private Vector3 _rippleMaxScale;
    [Foldout("Animation")]
    [SerializeField]
    private float _rippleTimeCoeff;
    private TMP_Text _tutorialText;
    private float _startShowingTime;
    private float _showingDuration;
    private bool _isTextAppearedCompletly;

    public event Action TextAnimationStoppedEvent;

    public void InitializeValues()
    {
        _tutorialText = GetComponent<TMP_Text>();
        _tutorialText.text = "";
        _startShowingTime = -1f;
        _isTextAppearedCompletly = false;
    }

    public void ShowTextForTime(string text, float showingDuration = float.PositiveInfinity)
    {
        _showingDuration = showingDuration;
        _tutorialText.text = text;
        //float fontSize = _tutorialText.fontSize;
        //_tutorialText.fontSize = 0f;
        DOTween.timeScale = 1f;
        _isTextAppearedCompletly = false;
        if (_appearDuration <= 0f)
        {
            _startShowingTime = Time.timeSinceLevelLoad;
            TextAnimationStoppedEvent?.Invoke();
            _isTextAppearedCompletly = true;
        }
        else
        {
            transform.DOPunchScale(_appearPunch, _appearDuration).onComplete = () =>
            {
                _startShowingTime = Time.timeSinceLevelLoad;
                TextAnimationStoppedEvent?.Invoke();
                _isTextAppearedCompletly = true;
            };
        }
    }

    public void StopShowingText()
    {
        if (_startShowingTime == -1f) return;
        _startShowingTime = -1f;
        _tutorialText.text = "";
    }

    public bool IsAnimStopped()
    {
        return _startShowingTime == -1f;
    }

    private void Update()
    {
        if (_startShowingTime == -1f) return;
        if (_isTextAppearedCompletly)
        {
            float rippleLerpCoeff = (Mathf.Sin((Time.unscaledTime - _startShowingTime) * _rippleTimeCoeff - 0.5f * Mathf.PI) + 1) / 2;
            transform.localScale = Vector3.Lerp(Vector3.one, _rippleMaxScale, rippleLerpCoeff);
        }
            if (Time.timeSinceLevelLoad - _startShowingTime >= _showingDuration)
        {
            StopShowingText();
        }
    }
}