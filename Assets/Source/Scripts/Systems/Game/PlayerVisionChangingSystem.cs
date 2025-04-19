

using Kuhpik;
using UnityEngine;
using DG.Tweening;

class PlayerVisionChangingSystem : GameSystem, IUpdating, IIniting
{
    private Renderer _playerVisionDrawRenderer;
    [SerializeField]
    private float _changeColorDuration;
    [SerializeField]
    private float _flickeringDurations;
    [SerializeField]
    private Color _greenZoneColor;
    [SerializeField]
    private Color _redZoneColor;
    private bool _isFlickerGreen = false;
    private bool _isFlickerDone = true;

    public void OnInit()
    {
        _playerVisionDrawRenderer = game.Player.HideableComponent.RadiusDraw.GetComponent<Renderer>();
    }

    public void OnUpdate()
    {
        if (!game.Player.HideableComponent.IsHiding) return;
        if (!game.ScareScreen.ScareSlider.gameObject.activeSelf)
        {
            if (_isFlickerGreen && _isFlickerDone)
            {
                _isFlickerDone = false;
                _playerVisionDrawRenderer.material.DOColor(_redZoneColor, _flickeringDurations).onComplete = () =>
                {
                    _isFlickerGreen = false;
                    _isFlickerDone = true;
                };
            } else if (!_isFlickerGreen && _isFlickerDone)
            {
                _isFlickerDone = false;
                _playerVisionDrawRenderer.material.DOColor(_greenZoneColor, _flickeringDurations).onKill = () =>
                {
                    _isFlickerGreen = true;
                    _isFlickerDone = true;
                };
            }
            return;
        }
        if (game.ScareScreen.ScareSlider.IsInGreenZone())
        {
            _playerVisionDrawRenderer.material.DOColor(_greenZoneColor, _changeColorDuration);
        }
        else
        {
            _playerVisionDrawRenderer.material.DOColor(_redZoneColor, _changeColorDuration);
        }
    }
}