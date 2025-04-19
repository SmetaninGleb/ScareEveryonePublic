using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePauseButton : MonoBehaviour
{
    public event Action PressedEvent;

    public void InitializeValues()
    {
        GetComponent<Button>().onClick.AddListener(ButtonPressed);
    }

    public void ButtonPressed()
    {
        PressedEvent?.Invoke();
    }
}
