using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public UnityEvent onChanged;

    private Dictionary<InventoryItem, int> items = new Dictionary<InventoryItem, int>();

    public void AddItem(InventoryItem item)
    {
        AddItems(item, 1);
    }

    public void AddItems(InventoryItem item, int quantity)
    {
        if (!items.ContainsKey(item))
        {
            items[item] = quantity;
        }
        else
        {
            items[item] += quantity;
        }

        onChanged.Invoke();
    }

    public void RemoveItem(InventoryItem item)
    {
        items[item]--;

        onChanged.Invoke();
    }

    public IEnumerable<(InventoryItem item, int quantity)> GetItems()
    {
        return items.Where(x => x.Value > 0).Select(x => (x.Key, x.Value));
    }
}
