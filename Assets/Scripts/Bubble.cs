using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [Header("Force Influences")]
    public float blowForceInfluence = 1;
    public float stickForceInfluence = 1;
    public float windForceInfluence = 1;
    public float baseSeekingForceInflucence = 1;
    [Space]
    public float maxSpeed = 3;
    //[SerializeField] private float steeringForce;
    [SerializeField] private Transform target;
    [SerializeField] private int hp;
    // need to add iframe timer
    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //WindController.blow += Move;

        target = GameObject.Find("Base").transform;
    }

    private void FixedUpdate()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        // Global wind
        rigidbody.AddForce(WindController.instance.direction * WindController.instance.strength * windForceInfluence);

        // Base
        rigidbody.AddForce((target.position - transform.position).normalized * baseSeekingForceInflucence);

        //var velocity = (target.position - transform.position).normalized * steeringForce;
        //transform.position += velocity * speedMultiplier;
        //speedMultiplier = 1;

        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);

        rigidbody.drag = 0;
    }

    //private void Move(Vector3 force)
    //{
    //    transform.position += force;
    //}

    public void TakeDamage(int DmgAmount)
    {
        Debug.Log("Hp: " + hp);
        hp -= DmgAmount;
    }

    public void Blow(Vector2 force)
    {
        rigidbody.AddForce(force * blowForceInfluence);
    }

    public void Stick(float strength)
    {
        //Stick(-rigidbody.velocity.normalized * strength);

        rigidbody.drag += strength;
    }

    //public void Stick(Vector2 force)
    //{
    //    rigidbody.AddForce(force * stickForceInfluence);
    //}
}
