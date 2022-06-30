using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingInventory : MonoBehaviour
{
    public List<InventoryItem> startingItems;

    private void Start()
    {
        var inventory = GameManager.instance.inventory;

        foreach (var item in startingItems)
        {
            inventory.AddItem(item);
        }
    }
}
