using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HurtArea : MonoBehaviour
{
    private const int maxColliders = 10;

    public UnityEvent onHurt;

    public float tickRate = 0.5f;
    public float damagePerSecond = 1;
    
    private new Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Tick), 0, tickRate);
    }

    private void Tick()
    {
        var damagePerTick = damagePerSecond * tickRate;

        var overlappingColliders = new Collider2D[maxColliders];

        var contactFilter = new ContactFilter2D();
        contactFilter.layerMask = LayerMask.GetMask("Bubble");

        var count = collider.OverlapCollider(contactFilter, overlappingColliders);

        var hasHurt = false;

        for (int i = 0; i < count; i++)
        {
            var collider = overlappingColliders[i];

            var bubble = collider.GetComponent<Bubble>();

            if (bubble == null)
            {
                continue;
            }

            hasHurt = true;

            bubble.Hurt(damagePerTick);
        }

        if (hasHurt)
        {
            onHurt.Invoke();
        }
    }
}
