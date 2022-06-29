using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BlowArea : MonoBehaviour
{
    public float strength = 1;

    private new Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        var overlappingColliders = new Collider2D[10];

        var count = collider.OverlapCollider(new ContactFilter2D(), overlappingColliders);

        for (int i = 0; i < count; i++)
        {
            var collider = overlappingColliders[i];

            var bubble = collider.GetComponent<Bubble>();

            if (bubble == null)
            {
                continue;
            }

            var falloff = 1 / (1 + Vector2.Distance(transform.position, bubble.transform.position));

            bubble.Blow(-transform.right * strength * falloff);
        }
    }
}
