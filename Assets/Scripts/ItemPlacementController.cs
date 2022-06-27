using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPlacementController : MonoBehaviour
{
    public UnityEvent<InventoryItem> onItemPlaced;

    [SerializeField]
    private GameObject itemPlacementPrefab;
    private ItemPlacement currentItemPlacement;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Place();
        }
    }

    public void Select(InventoryItem item)
    {
        if (currentItemPlacement != null)
        {
            Destroy(currentItemPlacement.gameObject);
        }

        currentItemPlacement = Instantiate(itemPlacementPrefab).GetComponent<ItemPlacement>();
        currentItemPlacement.item = item;
    }

    public void Place()
    {
        if (currentItemPlacement == null)
        {
            return;
        }

        var item = Instantiate(currentItemPlacement.item.prefab);
        item.transform.position = currentItemPlacement.transform.position;

        Destroy(currentItemPlacement.gameObject);
        
        onItemPlaced.Invoke(currentItemPlacement.item);
    }

    public void Cancel()
    {
        if (currentItemPlacement != null)
        {
            Destroy(currentItemPlacement.gameObject);
        }
    }
}
