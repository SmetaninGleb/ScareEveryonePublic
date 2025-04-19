using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    private Sprite _onSprite;
    [SerializeField]
    private Sprite _offSprite;
    private bool _isOn;
    private Image _buttonImage;

    public event Action SoundButtonPressedEvent;

    public void InitializeValues(bool isOn)
    {
        GetComponent<Button>().onClick.AddListener(ButtonPressed);
        _buttonImage = GetComponent<Image>();
        _isOn = isOn;
        if (_isOn)
        {
            _buttonImage.sprite = _onSprite;
        } else
        {
            _buttonImage.sprite = _offSprite;
        }
    }

    public void ButtonPressed()
    {
        ChangeSpriteState();
        SoundButtonPressedEvent?.Invoke();
    }

    private void ChangeSpriteState()
    {
        if (_isOn)
        {
            _buttonImage.sprite = _offSprite;
        }
        else
        {
            _buttonImage.sprite = _onSprite;
        }
        _isOn = !_isOn;
    }
}
