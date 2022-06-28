using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent onDied;

    public float maxHealth = 100;
    [HideInInspector]
    public float health;

    private void Start()
    {
        health = maxHealth;
    }

    public void Hit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            onDied.Invoke();
        }

        health = Mathf.Clamp(health, 0, maxHealth);
    }
}
