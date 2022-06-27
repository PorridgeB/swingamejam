using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryItem : ScriptableObject
{
    public string title;
    [TextArea]
    public string description;
    public Sprite icon;
    public GameObject prefab;
}
