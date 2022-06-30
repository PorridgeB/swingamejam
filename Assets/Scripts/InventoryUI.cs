using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    public UnityEvent<InventoryItem> onItemPressed;

    public Inventory inventory;
    [SerializeField]
    private GameObject inventoryItemUIPrefab;

    private void Start()
    {
        inventory.onChanged.AddListener(OnInventoryChanged);

        OnInventoryChanged();
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
            inventoryItemUI.transform.SetParent(transform, false);
            inventoryItemUI.transform.localScale = Vector3.one;
            inventoryItemUI.Setup(item.item, item.quantity);
            inventoryItemUI.onPressed.AddListener(() => onItemPressed.Invoke(item.item));
        }

        gameObject.SetActive(items.Any());
    }
}
