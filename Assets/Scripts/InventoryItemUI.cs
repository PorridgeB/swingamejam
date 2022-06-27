using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    public UnityEvent onPressed;

    [SerializeField]
    private TMP_Text quantity;
    [SerializeField]
    private TMP_Text title;
    private Image icon;
    private Button button;

    private void Awake()
    {
        icon = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(() => onPressed.Invoke());
    }

    public void Setup(InventoryItem item, int quantity)
    {
        icon.sprite = item.icon;
        title.text = item.title;
        this.quantity.text = quantity.ToString();
    }
}
