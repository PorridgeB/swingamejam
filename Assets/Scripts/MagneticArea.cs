using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MagneticArea : MonoBehaviour
{
    private const int maxColliders = 10;

    public Transform target;
    public float strength = 1;

    private new Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        var overlappingColliders = new Collider2D[maxColliders];

        var count = collider.OverlapCollider(new ContactFilter2D(), overlappingColliders);

        for (int i = 0; i < count; i++)
        {
            var collider = overlappingColliders[i];

            var bubble = collider.GetComponent<Bubble>();

            if (bubble == null)
            {
                continue;
            }

            if (bubble.magneticInfluence < 0.5f)
            {
                continue;
            }

            bubble.transform.position = Vector3.Lerp(bubble.transform.position, target.transform.position, strength * Time.fixedDeltaTime);

            //var deltaPosition = target.transform.position - bubble.transform.position;

            //if (deltaPosition.magnitude < 1)
            //{
            //    var rigidbody = bubble.GetComponent<Rigidbody2D>();

            //    rigidbody.AddForce(strength * -rigidbody.velocity);
            //}
            //else
            //{
            //    bubble.Attract(deltaPosition.normalized * strength);
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bubble = collision.GetComponent<Bubble>();

        if (bubble != null && bubble.magneticInfluence > 0.5f)
        {
            //bubble.overrideSteering = true;
            var rigidbody = bubble.GetComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;
            rigidbody.angularVelocity = 0;
            rigidbody.velocity = Vector2.zero;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    var bubble = collision.GetComponent<Bubble>();

    //    if (bubble != null && bubble.magneticInfluence > 0.5f)
    //    {
    //        //bubble.overrideSteering = false;
    //    }
    //}
}
