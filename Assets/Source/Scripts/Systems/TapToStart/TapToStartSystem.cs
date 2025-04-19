
using NaughtyAttributes;
using Kuhpik;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

class TapToStartSystem : GameSystem, IIniting
{
    [SerializeField]
    private Button _tapToStartButton;
    [Required]
    [SerializeField]
    private TMP_Text _tapToStartText;
    [Foldout("Text Animation")]
    [SerializeField]
    private float _appearDuration = 0.5f;
    [Foldout("Text Animation")]
    [SerializeField]
    private Vector3 _appearPunch = new Vector3(0.5f, 0.5f, 0.5f);

    public void OnInit()
    {
        _tapToStartText.transform.DOPunchScale(_appearPunch, _appearDuration);
        _tapToStartButton.onClick.AddListener(() => 
        {
            _tapToStartText.transform.DOPunchScale(_appearPunch, _appearDuration).onComplete = () => 
            {
                Bootstrap.ChangeGameState(EGamestate.Game);
            } ;
        });
    }
}