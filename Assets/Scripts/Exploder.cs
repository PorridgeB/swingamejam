using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [Tooltip("The bubble to spawn when exploding")]
    public GameObject bubblePrefab;
    [Tooltip("The number of bubbles to create")]
    public int count = 3;
    [Tooltip("The radius of the circle within which to create the bubbles")]
    public float radius = 0.5f;
    [Tooltip("The strength of the impulse given to the bubbles away from the center")]
    public float impulse = 1;

    public void Explode()
    {
        for (int i = 0; i < count; i++)
        {
            // Choose new random location near bubble
            var randomOffset = Random.insideUnitCircle * radius;

            var bubble = Instantiate(bubblePrefab, (Vector2)transform.position + randomOffset, Quaternion.Euler(0, 0, Random.Range(0, 360)));

            var rigidbody = bubble.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(randomOffset.normalized * impulse, ForceMode2D.Impulse);
        }
    }
}
