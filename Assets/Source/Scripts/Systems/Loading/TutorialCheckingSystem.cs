

using Kuhpik;

class TutorialCheckingSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        if (player.WasTutorial)
        {
            Bootstrap.ChangeGameState(EGamestate.TapToStart);
        } else
        {
            Bootstrap.ChangeGameState(EGamestate.Tutorial);
        }
    }
}