

using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

class TutorialShowingSystem : GameSystem, IIniting
{
    [Required]
    [SerializeField]
    private TutorialImageButton _tutorialImageButton;
    [SerializeField]
    private Sprite[] _tutorialSpriteSequence;
    private int _currentSpriteIndex;

    public void OnInit()
    {
        player.WasTutorial = true;
        _currentSpriteIndex = 0;
        _tutorialImageButton.InitializeValues();
        _tutorialImageButton.TutorialImageButtonClickedEvent += TutorialImageButtonClicked;
        TutorialImageButtonClicked();
    }

    public void TutorialImageButtonClicked()
    {
        if (_currentSpriteIndex == _tutorialSpriteSequence.Length)
        {
            Bootstrap.ChangeGameState(EGamestate.TapToStart);
        }
        _tutorialImageButton.ChangeImageSprite(_tutorialSpriteSequence[_currentSpriteIndex]);
        _currentSpriteIndex++;
    }
}