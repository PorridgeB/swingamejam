using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    [SerializeField]
    private GameObject inventoryItemUIPrefab;

    private void Start()
    {
        inventory.onChanged.AddListener(OnInventoryChanged);
    }

    private void OnInventoryChanged()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var items = inventory.GetItems();

        foreach (var item in items)
        {
            var inventoryItemUI = Instantiate(inventoryItemUIPrefab).GetComponent<InventoryItemUI>();
            inventoryItemUI.transform.SetParent(transform);
            inventoryItemUI.Setup(item.item, item.quantity);
        }
    }
}
