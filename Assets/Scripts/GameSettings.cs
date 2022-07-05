using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    [Tooltip("The level that the GameManager will load when entering the \"Game\" scene")]
    public Level currentLevel;
    [Range(0, 2)]
    public int difficulty;
}
