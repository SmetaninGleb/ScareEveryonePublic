

using Kuhpik;
using UnityEngine;

public class SoundOnOffSystem : GameSystem, IUpdating
{
    public void OnUpdate()
    {
        if (player.IsSoundOn)
        {
            AudioListener.volume = 1f;
        } else
        {
            AudioListener.volume = 0f;
        }
    }
}