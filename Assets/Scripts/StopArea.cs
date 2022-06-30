using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopArea : MonoBehaviour
{
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

            // Weird hack so that his stop area only applies to the iron bubble
            if (bubble.magneticInfluence < 0.5f)
            {
                continue;
            }

            var rigidbody = bubble.GetComponent<Rigidbody2D>();

            rigidbody.simulated = false;
        }
    }
}
