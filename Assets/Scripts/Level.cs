using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    public string sceneName;
    public List<InventoryItem> startingInventory;
}
