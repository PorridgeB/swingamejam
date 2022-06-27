using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public float rotationSnap = 15;
    public float rotationMinDistance = 0.1f;
    public Color canPlaceColor;
    public Color cantPlaceColor;
    [HideInInspector]
    public InventoryItem item;
    [HideInInspector]
    public bool canPlace = true;
    [HideInInspector]
    public bool rotate = false;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = item.icon;
        
        // Stop the sprite from appearing at the origin for a frame before "Update()"
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        spriteRenderer.enabled = true;

        spriteRenderer.color = canPlace ? canPlaceColor : cantPlaceColor;

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (rotate)
        {
            var deltaPosition = transform.position - mousePosition;

            if (deltaPosition.magnitude < rotationMinDistance)
            {
                return;
            }

            var angleToMouse = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg;
            angleToMouse = Mathf.Round(angleToMouse / rotationSnap) * rotationSnap;
            transform.rotation = Quaternion.Euler(0, 0, angleToMouse);
        }
        else
        {
            transform.position = mousePosition;
        }
    }
}
