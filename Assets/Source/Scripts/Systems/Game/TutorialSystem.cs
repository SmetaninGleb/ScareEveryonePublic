

using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

class TutorialSystem : GameSystem, IUpdating, IIniting
{
    private TutorialComponent _tutorial;
    [Required]
    [SerializeField]
    private TutorialEventButton _tutorialEventButton;
    [Required]
    [SerializeField]
    private GameObject _tutorialScreen;
    private ETutor _currentTutor;
    private bool _wasButtonClicked = false;
    private ArrowPointerComponent _arrowPointer;
    [Required]
    [SerializeField]
    private TutorialSliderPointer _sliderPointer;

    public void OnInit()
    {
        _sliderPointer.gameObject.SetActive(false);
        if (player.WasTutorial || !config.LevelContainer.Levels[player.CurrentLevelIndex].IsTutorial)
        {
            player.WasTutorial = true;
            _tutorialScreen.gameObject.SetActive(false);
            return;
        }
        _tutorial = FindObjectOfType<TutorialComponent>();
        _arrowPointer = FindObjectOfType<ArrowPointerComponent>();
        _arrowPointer.gameObject.SetActive(false);
        _tutorial.InitializeValues();
        _tutorialEventButton.InitializeValues();
        _tutorialEventButton.TutorialClickedEvent += TutorialChangeBtnCliked;
        _tutorialEventButton.gameObject.SetActive(false);
    }

    public void OnUpdate()
    {
        if (player.WasTutorial) return;
        foreach (PeacefulMobComponent mob in game.PeacefulMobList)
        {
            if (mob.IsAngry || mob.IsRunning)
            {
                Bootstrap.GameRestart(0);
            }
        }
        if (_currentTutor == ETutor.Initial)
        {
            _tutorial.ShowTutor(ETutor.Moving);
            _currentTutor = ETutor.Moving;
        }
        if (_currentTutor == ETutor.Moving && game.Player.IsMoving)
        {
            _tutorial.ShowTutor(ETutor.Hiding);
            _currentTutor = ETutor.Hiding;
            //StartListeningTutorButton();
            _arrowPointer.gameObject.SetActive(true);
            _arrowPointer.StartArrowMoving();
        }
        if (_currentTutor == ETutor.Hiding && _wasButtonClicked)
        {
            //StopListeningTutorButton();
            _tutorial.StartGameTime();
        }
        if (_currentTutor == ETutor.Hiding && game.Player.HideableComponent.IsHiding && game.ScareScreen.ScareSlider.IsInGreenZone())
        {
            _tutorial.ShowTutor(ETutor.Scaring);
            _currentTutor = ETutor.Scaring;
            _arrowPointer.gameObject.SetActive(false);
            StartListeningTutorButton();
            _sliderPointer.gameObject.SetActive(true);
        }
        if (_currentTutor == ETutor.Scaring && _wasButtonClicked)
        {
            game.ScareScreen.ScareButton.onClick?.Invoke();
            StopListeningTutorButton();
            _tutorial.StartGameTime();
        }
        if (_currentTutor == ETutor.Scaring && !game.Player.HideableComponent.IsHiding)
        {
            _sliderPointer.gameObject.SetActive(false);
            //player.WasTutorial = true;
            Bootstrap.ChangeGameState(EGamestate.Win);
        }
        //if (_currentTutor == ETutor.MobsAngry && _wasButtonClicked)
        //{
        //    _tutorial.StartGameTime();
        //    _wasButtonClicked = false;
        //    StopListeningTutorButton();
        //}
    }

    public void TutorialChangeBtnCliked()
    {
        _wasButtonClicked = true;
    }

    private void StartListeningTutorButton()
    {
        _tutorialEventButton.gameObject.SetActive(true);
        game.Joystick.gameObject.SetActive(false);
    }

    private void StopListeningTutorButton()
    {
        _wasButtonClicked = false;
        _tutorialEventButton.gameObject.SetActive(false);
        game.ShouldReloadJoystick = true;
    }
}