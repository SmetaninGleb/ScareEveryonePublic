using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScareScreen : MonoBehaviour
{
    public ScareSlider ScareSlider;
    public Button ScareButton;

    public event Action TappedToScareEvent;

    public void TappedToScare()
    {
        TappedToScareEvent?.Invoke();
    }
}
