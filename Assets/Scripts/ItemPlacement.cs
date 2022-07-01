using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPlacement : MonoBehaviour
{
    public UnityEvent onRotated;

    public float rotationSnap = 20;
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
        spriteRenderer.sprite = item.placementSprite;
        
        // Stop the sprite from appearing at the origin for a frame before "Update()"
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        var overlappingCollider = Physics2D.OverlapCircle(transform.position, item.size, LayerMask.GetMask("Tower", "Obstacle"));
        canPlace = overlappingCollider == null;

        spriteRenderer.enabled = true;

        spriteRenderer.color = canPlace ? canPlaceColor : cantPlaceColor;

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (rotate && item.canRotate)
        {
            var deltaPosition = transform.position - mousePosition;

            if (deltaPosition.magnitude < rotationMinDistance)
            {
                return;
            }

            var angleToMouse = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg;
            angleToMouse = Mathf.Round(angleToMouse / rotationSnap) * rotationSnap;

            var previousRotation = transform.rotation;

            transform.rotation = Quaternion.Euler(0, 0, angleToMouse);

            // The item was rotated
            if (previousRotation != transform.rotation)
            {
                onRotated.Invoke();
            }
        }
        else
        {
            transform.position = mousePosition;
        }
    }
}
