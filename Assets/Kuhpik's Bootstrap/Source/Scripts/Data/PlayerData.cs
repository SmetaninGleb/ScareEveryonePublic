using NaughtyAttributes;
using System;

namespace Kuhpik
{
    /// <summary>
    /// Used to store player's data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        // Example (I use public fields for data, but u free to use properties\methods etc)
        // [BoxGroup("level")] public int level;
        // [BoxGroup("currency")] public int money;
        public int CurrentLevelIndex;
        public bool IsLevelRandom = false;
        public bool WasTutorial = false;

        public bool IsVibroOn = true;
        public bool IsSoundOn = true;
    }
}