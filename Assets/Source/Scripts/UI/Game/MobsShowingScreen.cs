using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using DG.Tweening;

public class MobsShowingScreen : MonoBehaviour
{
    [Required]
    [SerializeField]
    private float _pixelsBetweenImg;
    [Required]
    [SerializeField]
    private Sprite _smileSprite;
    [Required]
    [SerializeField]
    private Sprite _deadSprite;
    [Required]
    [SerializeField]
    private GameObject _imgPrefab;
    private List<GameObject> _imgs = new List<GameObject>();
    private int _deadImgStartIndex = 0;
    [Foldout("Animations")]
    [SerializeField]
    private float _punchDuration;
    [Foldout("Animations")]
    [SerializeField]
    private Vector3 _punchScale;
    private VerticalLayoutGroup _layout;

    public void DrawImgs(int mobsNum)
    {
        _layout = GetComponent<VerticalLayoutGroup>();
        for (int i = 0; i < mobsNum; i++)
        {
            //float xCoor = (i - mobsNum / 2f + 0.5f) * _pixelsBetweenImg;
            GameObject currentObj = Instantiate(_imgPrefab, transform);
            _imgs.Add(currentObj);
            //((RectTransform)currentObj.transform).anchoredPosition = new Vector2(xCoor, 0f);
        }
        UpdateDeadNumber(0);
    }

    public void UpdateDeadNumber(int newDeadNumber)
    {
        if (_deadImgStartIndex == newDeadNumber || _deadImgStartIndex == _imgs.Count) return;
        for (int i = _deadImgStartIndex; i < newDeadNumber; i++)
        {
            _imgs[i].GetComponent<Image>().sprite = _deadSprite;
            _imgs[i].transform.DOPunchScale(_punchScale, _punchDuration);
        }
        _deadImgStartIndex = newDeadNumber;
    } 
}
