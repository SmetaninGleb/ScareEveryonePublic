namespace Kuhpik
{
    public enum EGamestate
    {
        // Don't change int values in the middle of development.
        // Otherwise all of your settings in inspector can be messed up.

        Loading = 1,
        TapToStart = 2,
        Tutorial = 5,
        Game = 3,
        Win = 4,
        Lose = 10,
        

        // Extend just like that
        //
        // Shop = 20,
        // Settings = 30,
        // Revive = 100
    }
}