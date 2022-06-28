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
    [SerializeField] private bool spawnBubblesOnDeath;
    [SerializeField] private GameObject babyBubblePrefab;
    private bool damageable;

    private void Start()
    {
        WindController.blow += Move;
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
                    GameObject newBubble = Instantiate(babyBubblePrefab, pos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    //Rigidbody2D rb = newBubble.GetComponent<Rigidbody2D>();
                    //rb.AddForce(Vector2.right * 100000, ForceMode2D.Impulse);
                    
                }
            }
            Destroy(gameObject);
        }
        //bubble movement
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
        if (damageable)
        {
            //Debug.Log("Hp: " + hp);
            hp -= DmgAmount;
        }
    }
}
