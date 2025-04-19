using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComponent : MonoBehaviour
{
    [Min(1f)]
    public float TimeToLose;
    [Min(1)]
    public int MobsToScareForWin;
    public bool IsTutorial = false;
}
