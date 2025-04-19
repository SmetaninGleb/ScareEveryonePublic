using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageComponent : MonoBehaviour
{
    [SerializeField]
    private float _existDuration;
    private float _appearStartTime;


    public void InitializeValues()
    {
        gameObject.SetActive(false);
    }

    public void AppearGarbage()
    {
        gameObject.SetActive(true);
        _appearStartTime = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Time.timeSinceLevelLoad - _appearStartTime >= _existDuration)
            {
                transform.parent.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
