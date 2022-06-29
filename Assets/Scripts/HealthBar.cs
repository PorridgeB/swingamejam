using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health;
    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private TMP_Text text;

    private void Update()
    {
        if (health == null)
        {
            return;
        }

        foregroundImage.fillAmount = health.percentage;

        if (text != null)
        {
            text.text = $"{health.health}/{health.maxHealth}";
        }
    }
}
