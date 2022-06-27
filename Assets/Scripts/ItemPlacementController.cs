using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemPlacementController : MonoBehaviour
{
    public UnityEvent<InventoryItem> onItemPlaced;

    [SerializeField]
    private GameObject itemPlacementPrefab;
    private ItemPlacement currentItemPlacement;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (currentItemPlacement == null)
        {
            return;
        }

        currentItemPlacement.rotate = Input.GetMouseButton(0);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }

        if (Input.GetMouseButtonUp(0))
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
        item.transform.SetPositionAndRotation(currentItemPlacement.transform.position, currentItemPlacement.transform.rotation);

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
