using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlacePointerComponent : MonoBehaviour
{
    [SerializeField]
    private HidePlacePointerPartComponent[] _pointerPartList;
    private bool _isPointing;

    public void InitializeValues()
    {
        foreach (HidePlacePointerPartComponent part in _pointerPartList)
        {
            part.InitializeValues();
            _isPointing = false;
        }
    }

    public void ShowPointer()
    {
        if (_isPointing) return;
        _isPointing = true;
        foreach (HidePlacePointerPartComponent part in _pointerPartList)
        {
            part.Show();
        }
    }

    public void DisablePointer()
    {
        if (!_isPointing) return;
        _isPointing = false;
        foreach (HidePlacePointerPartComponent part in _pointerPartList)
        {
            part.Disable();
        }
    }
}
