using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenEffect : Effect
{
    [SerializeField]
    private GameObject shatterPrefab;

    private void Start()
    {
        // Stop the bubble
        var rigidbody = bubble.GetComponent<Rigidbody2D>();
        rigidbody.angularVelocity = 0;
        rigidbody.velocity = Vector2.zero;

        bubble.overrideSteering = true;
    }

    protected override void OnExpire()
    {
        base.OnExpire();

        bubble.overrideSteering = false;

        var shatter = Instantiate(shatterPrefab);
        shatter.transform.position = transform.position;

        var particleSystem = shatter.GetComponent<ParticleSystem>();
        particleSystem.Play();
    }
}
