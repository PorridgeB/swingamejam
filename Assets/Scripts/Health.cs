using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent onDied;
    public UnityEvent onHurt;

    [Min(0)]
    public float maxHealth = 10;
    public bool invulnerable = false;

    public float health { get; private set; }
    public float percentage => Mathf.Clamp01(health / maxHealth);

    private void Start()
    {
        health = maxHealth;
    }

    public void Reset()
    {
        health = maxHealth;
    }

    public void Hurt(float damage)
    {
        if (invulnerable)
        {
            return;
        }

        health -= damage;

        onHurt.Invoke();

        if (health <= 0)
        {
            onDied.Invoke();
        }

        health = Mathf.Clamp(health, 0, maxHealth);
    }
}
