using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text quantity;
    private Image icon;

    private void Awake()
    {
        icon = GetComponent<Image>();
    }

    public void Setup(InventoryItem item, int quantity)
    {
        icon.sprite = item.icon;
        this.quantity.text = quantity.ToString();
    }
}
