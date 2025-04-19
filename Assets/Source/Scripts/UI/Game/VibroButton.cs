using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibroButton : MonoBehaviour
{
    [SerializeField]
    private Sprite _onSprite;
    [SerializeField]
    private Sprite _offSprite;
    private bool _isOn;
    private Image _buttonImage;
    
    public event Action VibroButtonPressedEvent;

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
        VibroButtonPressedEvent?.Invoke();
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
