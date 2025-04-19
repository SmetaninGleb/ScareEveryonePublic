using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using DG.Tweening;

public class TutorialImageButton : MonoBehaviour
{
    private Image _image;
    private Button _button;
    [Foldout("Animation")]
    [Range(0.01f, 2f)]
    [SerializeField]
    private float _appearingDuration;
    [Foldout("Animation")]
    [SerializeField]
    private Vector3 _punch;

    public event Action TutorialImageButtonClickedEvent;
    
    public void InitializeValues()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonCliked);
    }

    public void ChangeImageSprite(Sprite sprite)
    {
        _image.sprite = sprite;
        _image.color = new Color(255f, 255f, 255f, 0f);
        _image.DOColor(Color.white, _appearingDuration);
        transform.DOPunchScale(_punch, _appearingDuration, vibrato:3);
    }

    private void ButtonCliked()
    {
        TutorialImageButtonClickedEvent?.Invoke();
    }
}
