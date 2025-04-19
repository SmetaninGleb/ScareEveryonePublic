
using UnityEngine;
using Kuhpik;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

class ShowTimerSystem : GameSystem, IUpdating
{
    [Required]
    [SerializeField]
    private TMP_Text TimerText;
    [Required]
    [SerializeField]
    private Image TimerImage;

    public void OnUpdate()
    {
        int gameTimeSeconds = (int)(game.TimeToLose - (Time.timeSinceLevelLoad - game.StartGameTime));
        if (gameTimeSeconds % 60 >= 10)
        {
            TimerText.text = string.Format("{0}:{1}", gameTimeSeconds / 60, gameTimeSeconds % 60);
        }
        else
        {
            TimerText.text = string.Format("{0}:0{1}", gameTimeSeconds / 60, gameTimeSeconds % 60);
        }
        TimerImage.fillAmount = 1f - (Time.timeSinceLevelLoad - game.StartGameTime) / game.TimeToLose;
    }
}