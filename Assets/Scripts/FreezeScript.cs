using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeScript : MonoBehaviour
{
    private const int maxColliders = 10;

    public float tickRate = 4f;

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

        var overlappingColliders = new Collider2D[maxColliders];

        var contactFilter = new ContactFilter2D();
        contactFilter.layerMask = LayerMask.GetMask("Bubble");

        var count = collider.OverlapCollider(contactFilter, overlappingColliders);

        for (int i = 0; i < count; i++)
        {
            var collider = overlappingColliders[i];

            var bubble = collider.GetComponent<Bubble>();

            if (bubble == null)
            {
                continue;
            }

            bubble.frozen = true;
        }
    }
}
