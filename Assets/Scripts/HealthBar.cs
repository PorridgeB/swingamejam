using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health;
    public Image foregroundImage;

    private void Update()
    {
        foregroundImage.fillAmount = health.percentage;
    }
}
