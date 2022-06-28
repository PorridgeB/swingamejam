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

    private void Start()
    {
        WindController.blow += Move;

        target = GameObject.Find("Base").transform;
    }

    private void FixedUpdate()
    {
        if (hp <= 0)
        {
            int i = 0;
            int bubbleSpawnAmt = 3;
            Destroy(gameObject);
            if (spawnBubblesOnDeath)
            {
                while(i < bubbleSpawnAmt)
                {
                    // choose new random location near bubble

                    // create baby bubble
                    Debug.Log("baby bubble created");
                    Instantiate(babyBubblePrefab, gameObject.transform);
                    i++;
                }
            }
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
