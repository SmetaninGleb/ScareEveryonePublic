using Kuhpik;
using UnityEngine;

class TapToHideButtonLoading : GameSystem, IIniting
{
    [SerializeField]
    private TapToHideButton _tapToHideButton;

    public void OnInit()
    {
        game.TapToHideButton = _tapToHideButton;
        game.TapToHideButton.gameObject.SetActive(false);
    }
}