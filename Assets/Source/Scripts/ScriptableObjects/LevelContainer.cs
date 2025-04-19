using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelContainer", menuName = "Config/Level Container")]
public class LevelContainer : ScriptableObject
{
    [SerializeField]
    public LevelComponent[] Levels;
}
