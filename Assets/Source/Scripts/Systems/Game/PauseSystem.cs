

using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class PauseSystem : GameSystem, IIniting
{
    [Required]
    [SerializeField]
    private PauseScreen _pauseScreen;
    [Required]
    [SerializeField]
    private PauseButton _pauseButton;
    [Required]
    [SerializeField]
    private VibroButton _vibroButton;
    [Required]
    [SerializeField]
    private SoundButton _soundButton;
    [Required]
    [SerializeField]
    private ClosePauseButton _closePauseButton;
    [Required]
    [SerializeField]
    private MobsShowingScreen _mobShowingScreen;

    public void OnInit()
    {
        _pauseScreen.gameObject.SetActive(false);
        _pauseButton.InitializeValues();
        _pauseButton.PauseButtonPressedEvent += ChangePauseScreenState;
        _closePauseButton.InitializeValues();
        _closePauseButton.PressedEvent += ChangePauseScreenState;
        _vibroButton.InitializeValues(player.IsVibroOn);
        _vibroButton.VibroButtonPressedEvent += ChangeVibroState;
        _soundButton.InitializeValues(player.IsSoundOn);
        _soundButton.SoundButtonPressedEvent += ChangeSoundState;
    }

    public void ChangeVibroState()
    {
        player.IsVibroOn = !player.IsVibroOn;
    }

    public void ChangeSoundState()
    {
        player.IsSoundOn = !player.IsSoundOn;
    }
    
    public void ChangePauseScreenState()
    {
        if (!_pauseScreen.gameObject.activeSelf)
        {
            _pauseScreen.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(false);
            Time.timeScale = 0f;
            //_mobShowingScreen.gameObject.SetActive(false);
        } else
        {
            _pauseScreen.gameObject.SetActive(false);
            _pauseButton.gameObject.SetActive(true);
            Time.timeScale = 1f;
            //_mobShowingScreen.gameObject.SetActive(true);
        }
    }
}