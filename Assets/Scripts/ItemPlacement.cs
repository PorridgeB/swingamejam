using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public Color canPlaceColor;
    public Color cantPlaceColor;
    [HideInInspector]
    public InventoryItem item;
    [HideInInspector]
    public bool canPlace = true;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = item.icon;
    }

    private void Update()
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        transform.position = worldPosition;

        spriteRenderer.color = canPlace ? canPlaceColor : cantPlaceColor;
    }
}
