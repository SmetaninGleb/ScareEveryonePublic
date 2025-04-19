using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public class TutorialImage : MonoBehaviour
{
    private Image _image;
    
    public void InitializeValues()
    {
        _image = GetComponent<Image>();
    }

    public void SetSprite(Sprite newSprite)
    {
        _image.sprite = newSprite;
        _image.preserveAspect = true;
    }

    public void ClearSprite()
    {
        _image.sprite = null;
    }
}
