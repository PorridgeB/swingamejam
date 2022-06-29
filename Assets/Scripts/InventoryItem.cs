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
    [Header("Placement")]
    public GameObject prefab;
    public Sprite placementSprite;
    public bool canRotate = true;
    public float size = 1;
}
