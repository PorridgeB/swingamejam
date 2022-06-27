using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Inventory inventory;
    public List<InventoryItem> startingItems;

    private void Start()
    {
        foreach (var startingItem in startingItems)
        {
            inventory.AddItem(startingItem);
        }
    }
}
