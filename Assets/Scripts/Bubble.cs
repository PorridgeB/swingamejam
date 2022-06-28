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
    [SerializeField] private bool spawnBubblesOnDeath;
    [SerializeField] private GameObject babyBubblePrefab;
    private bool damageable;
    // need to add iframe timer
    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //WindController.blow += Move;
        damageable = true;
        target = GameObject.Find("Base").transform;
    }

    private void FixedUpdate()
    {
        //on death
        if (hp <= 0)
        {
            int bubbleSpawnAmt = 3;
            //spawn baby bubbles
            if (spawnBubblesOnDeath)
            {
                for(int i = 0; i < bubbleSpawnAmt; i++)
                {
                    // choose new random location near bubble
                    // create baby bubble
                    //Debug.Log("baby bubble created");
                    Vector2 pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                    GameObject newBubble = Instantiate(babyBubblePrefab, 
                        new Vector2(pos.x + Random.Range(-0.5f, 0.5f), pos.y + Random.Range(-0.5f, 0.5f)), 
                        Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    //Rigidbody2D rb = newBubble.GetComponent<Rigidbody2D>();
                    //rb.AddForce(Vector2.right * 100000, ForceMode2D.Impulse);
                    
                }
            }
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
        if (damageable)
        {
            //Debug.Log("Hp: " + hp);
            hp -= DmgAmount;
        }
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
