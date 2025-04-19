using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialComponent : MonoBehaviour
{
    [SerializeField]
    private string _movingTutorMessage;
    [SerializeField]
    private float _movingTutorTime;
    [SerializeField]
    private Sprite _movingTutorSprite;
    [SerializeField]
    private string _hidingTutorMessage;
    [SerializeField]
    private float _hidingTutorTime;
    [SerializeField]
    private string _scareTutorMessage;
    [SerializeField]
    private float _scareTutorTime;
    [SerializeField]
    private string _mobsAngryTutorMessage;
    [SerializeField]
    private float _mobsAngryTutorTime;
    private ETutor _currentTutor;
    private TutorialText _tutorialText;
    private TutorialImage _tutorialImage;

    public TutorialText TutorialText => _tutorialText;
    
    public void InitializeValues()
    {
        _tutorialText = FindObjectOfType<TutorialText>();
        _tutorialText.InitializeValues();
        _currentTutor = ETutor.Initial;
        _tutorialImage = FindObjectOfType<TutorialImage>();
        _tutorialImage.InitializeValues();
        _tutorialImage.gameObject.SetActive(false);
    }

    public void ShowTutor(ETutor tutor)
    {
        if (_currentTutor == tutor) return;
        _currentTutor = tutor;
        string tutorText;
        float tutorDuration;
        _tutorialImage.gameObject.SetActive(false);
        if (tutor == ETutor.Moving)
        {
            tutorText = _movingTutorMessage;
            tutorDuration = _movingTutorTime;
            _tutorialImage.gameObject.SetActive(true);
            _tutorialImage.SetSprite(_movingTutorSprite);
        }
        else if (tutor == ETutor.Hiding)
        {
            tutorText = _hidingTutorMessage;
            tutorDuration = _hidingTutorTime;
            //_tutorialText.TextAnimationStoppedEvent += () => Time.timeScale = 0f;
        }
        else if (tutor == ETutor.Scaring)
        {
            tutorText = _scareTutorMessage;
            tutorDuration = _scareTutorTime;
            _tutorialText.TextAnimationStoppedEvent += () => Time.timeScale = 0f;
        }
        else if (tutor == ETutor.MobsAngry)
        {
            tutorText = _mobsAngryTutorMessage;
            tutorDuration = _mobsAngryTutorTime;
            _tutorialText.TextAnimationStoppedEvent += () => Time.timeScale = 0f;
        }
        else return;
        _tutorialText.StopShowingText();
        if (tutorDuration == 0f) _tutorialText.ShowTextForTime(tutorText);
        else _tutorialText.ShowTextForTime(tutorText, tutorDuration);
    }

    public void StartGameTime()
    {
        Time.timeScale = 1f;
        _tutorialText.StopShowingText();
    }

    public bool IsTutorStopped()
    {
        return _tutorialText.IsAnimStopped();
    }
}
public enum ETutor
{
    Initial,
    Moving,
    Hiding,
    Scaring,
    MobsAngry
}
