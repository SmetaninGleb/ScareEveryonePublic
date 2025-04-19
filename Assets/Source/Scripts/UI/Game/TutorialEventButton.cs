using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEventButton : MonoBehaviour
{
    public event Action TutorialClickedEvent;

    public void InitializeValues()
    {
        GetComponent<Button>().onClick.AddListener(ButtonCliked);
    }

    private void ButtonCliked()
    {
        TutorialClickedEvent?.Invoke();
    }
}