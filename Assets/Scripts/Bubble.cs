using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    // Controls the speed of the bubble, and is reset to 1 at the end of every fixed-update.
    public float speedMultiplier = 1;

    [SerializeField] private float steeringForce;
    [SerializeField] private Transform target;
    [SerializeField] private int hp;
    // need to add iframe timer

    private void Start()
    {
        WindController.blow += Move;

        target = GameObject.Find("Base").transform;
    }

    private void FixedUpdate()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        var velocity = (target.position - transform.position).normalized * steeringForce;

        transform.position += velocity * speedMultiplier;

        speedMultiplier = 1;
    }

    private void Move(Vector3 force)
    {
        transform.position += force;
    }

    public void TakeDamage(int DmgAmount)
    {
        Debug.Log("Hp: " + hp);
        hp -= DmgAmount;
    }
}
