

using Kuhpik;
using UnityEngine;

class SoundTurningLoading : GameSystem, IIniting
{
    public void OnInit()
    {
        if (player.IsSoundOn)
        {
            AudioListener.volume = 1f;
        }
        else
        {
            AudioListener.volume = 0f;
        }
    }
}