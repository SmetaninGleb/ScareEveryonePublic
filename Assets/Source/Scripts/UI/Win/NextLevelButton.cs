using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    public event Action NextLevelButtonEvent;

    public void NextLevelButtonPressed()
    {
        NextLevelButtonEvent?.Invoke();
    }
}
