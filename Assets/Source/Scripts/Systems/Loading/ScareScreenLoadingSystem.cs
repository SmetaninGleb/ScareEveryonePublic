
using UnityEngine;
using Kuhpik;

class ScareScreenLoadingSystem : GameSystem, IIniting
{
    public ScareScreen ScareScreen;
    public void OnInit()
    {
        game.ScareScreen = ScareScreen;
        ScareScreen.ScareSlider.InitializeValues();
        ScareScreen.gameObject.SetActive(false);
    }
}