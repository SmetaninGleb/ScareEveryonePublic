using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryAgainButton : MonoBehaviour
{
    public event Action TryAgainButtonTappedEvent;

    public void TryAgainButtonTapped()
    {
        TryAgainButtonTappedEvent?.Invoke();
    }
}
