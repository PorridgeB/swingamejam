using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Base : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Start()
    {
        health.onDied.AddListener(() => Debug.Log("Game over!"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bubble = collision.GetComponent<Bubble>();

        if (bubble == null)
        {
            return;
        }

        bubble.Pop();

        health.Hurt(bubble.damageOnImpact);
    }
}
