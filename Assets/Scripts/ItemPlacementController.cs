using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemPlacementController : MonoBehaviour
{
    public UnityEvent<InventoryItem> onItemPlaced;
    public UnityEvent<InventoryItem> onItemGrabbed;
    public UnityEvent onItemPlacementFailed;
    public UnityEvent onItemRotated;
    public bool canGrab = true;

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
            if (Input.GetMouseButtonUp(0) && canGrab)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                var hitInfo = Physics2D.GetRayIntersection(ray, Mathf.Infinity, LayerMask.GetMask("Tower"));

                if (hitInfo.collider != null)
                {
                    var tower = hitInfo.collider.GetComponent<Tower>();

                    if (tower.placedByPlayer)
                    {
                        Select(tower.item);
                        currentItemPlacement.itemSprite.transform.rotation = tower.transform.rotation;

                        Destroy(tower.gameObject);

                        onItemGrabbed.Invoke(tower.item);
                    }
                }
            }
        }
        else
        {
            UpdateItemPlacement();
        }
    }

    private void UpdateItemPlacement()
    {
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
        currentItemPlacement.onRotated.AddListener(() => onItemRotated.Invoke());
    }

    public void Place()
    {
        if (currentItemPlacement == null)
        {
            return;
        }

        if (!currentItemPlacement.canPlace)
        {
            onItemPlacementFailed.Invoke();

            return;
        }

        var item = Instantiate(currentItemPlacement.item.prefab);
        item.transform.SetPositionAndRotation(currentItemPlacement.transform.position, currentItemPlacement.itemRotation);

        var tower = item.GetComponent<Tower>();
        tower.placedByPlayer = true;

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

    public void SetCanGrab(bool grab)
    {
        canGrab = grab;
    }
}
