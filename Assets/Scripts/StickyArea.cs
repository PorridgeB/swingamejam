using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StickyArea : MonoBehaviour
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

        var count = collider.OverlapCollider(new ContactFilter2D().NoFilter(), overlappingColliders);

        for (int i = 0; i < count; i++)
        {
            var collider = overlappingColliders[i];

            var bubble = collider.GetComponent<Bubble>();

            if (bubble == null)
            {
                return;
            }

            bubble.speedMultiplier *= 1 / (1 + strength);
        }
    }

    //private void OnCollisionStay2D(Collider2D collision)
    //{
    //    var bubble = collision.GetComponent<Bubble>();

    //    if (bubble == null)
    //    {
    //        return;
    //    }

    //    bubble.speedMultiplier *= 1 / (1 + strength);
    //}
}
