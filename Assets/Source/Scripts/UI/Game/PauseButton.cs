using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public event Action PauseButtonPressedEvent;

    public void InitializeValues()
    {
        GetComponent<Button>().onClick.AddListener(ButtonPressed);
    }

    public void ButtonPressed()
    {
        PauseButtonPressedEvent?.Invoke();
    }
    
}
