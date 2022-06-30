using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    [Tooltip("The name of the scene that the GameManager will load when entering the \"Game\" scene")]
    public string levelName;
    [Range(0, 3)]
    public int difficulty;
}
