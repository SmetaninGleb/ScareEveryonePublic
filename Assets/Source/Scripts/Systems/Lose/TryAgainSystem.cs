

using Kuhpik;
using UnityEngine.UI;
using UnityEngine;
using GameAnalyticsSDK;

class TryAgainSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        TryAgainButton tryAgainButton = FindObjectOfType<TryAgainButton>();
        tryAgainButton.GetComponent<Button>().onClick.AddListener(TryAgain);

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, player.CurrentLevelIndex.ToString());
    }

    public void TryAgain()
    {
        Bootstrap.GameRestart(0);
    }
}