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
    public float damageOnImpact = 2;

    //[SerializeField] private float steeringForce;
    [SerializeField] private Transform target;
    [SerializeField] private int hp;
    [SerializeField] private bool spawnBubblesOnDeath;
    [SerializeField] private GameObject babyBubblePrefab;
    [SerializeField] private float stuckTime;
    [SerializeField] private GameObject popParticle;

    private bool damageable;
    private float damageStartTime;
    SpriteRenderer bubbleSprite;
    [SerializeField] private Gradient bubbleColor;
    [SerializeField] private LayerMask steeringMask;
    [SerializeField] private List<Vector2> raycastDirections;
    [SerializeField] private List<bool> validDirections;
    [SerializeField] private float seperationStrength;

    private bool stuck;
    private float lastTime;
    public float speed;
    public float speedPerSecond;
    public Vector3 oldPosition;

    public Vector2 flowDirection = Vector3.right;
    
    private Vector2 TargetDirection => (target.position - transform.position).normalized;

    public bool Stuck { get => stuck; set => stuck = value; }

    private Vector2 currentDir;
    private Rigidbody2D rb;

    private void Start()
    {
        //WindController.blow += Move;
        damageable = true;
        target = GameObject.Find("Base").transform;
        currentDir = TargetDirection;

        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = bubbleColor.Evaluate(Random.Range(0f, 1f));
        bubbleSprite = GetComponent<SpriteRenderer>();
        Stuck = false;
        stuckTime = 5f;
        lastTime = 0;
    }

    private void FixedUpdate()
    {
        //on death
        if (hp <= 0)
        {
            Pop();
        }

        // Global wind
        //rb.AddForce(WindController.instance.direction * WindController.instance.strength * windForceInfluence);

        // Base
        rb.drag = 1;
        //rb.AddForce(FindDesiredDirection());
        rb.AddForce(flowDirection * windForceInfluence);

        SpreadOut();

        rb.velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, maxSpeed);

        // bubble turns red when hit 
        if (damageStartTime != 0)
        {
            float gbColor = (Time.timeSinceLevelLoad - damageStartTime) / 0.5f;

            bubbleSprite.color = new Color(1, gbColor, gbColor, 1);

            if (gbColor > 1)
            {
                damageStartTime = 0;
                bubbleSprite.color = Color.white;
            }
        }

        //checks if the bubble has barely moved for roughly 5 seconds
        CheckStuck();
    }

    private Vector2 FindDesiredDirection()
    {
        Vector2 desiredDirection = new Vector2();
        foreach (Vector2 dir in raycastDirections)
        {
            validDirections[raycastDirections.IndexOf(dir)] = false;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 2, steeringMask);
            if (!hit)
            {

                validDirections[raycastDirections.IndexOf(dir)] = true;
                if (Vector2.Dot(desiredDirection, TargetDirection) < Vector2.Dot(dir, TargetDirection))
                {
                    desiredDirection = dir;
                }
            }
        }
        if (desiredDirection.x == 0 || currentDir.x == 0)
        {
            desiredDirection = currentDir;
        }
        currentDir = desiredDirection;
        return desiredDirection;
    }

    private void SpreadOut()
    {
        foreach (Bubble b in FindObjectsOfType<Bubble>())
        {
            rb.AddForce((transform.position-b.transform.position).normalized * seperationStrength * Mathf.Clamp(1 - Vector2.Distance(transform.position, b.transform.position), 0, 1));
        }
    }

    public void TakeDamage(int DmgAmount)
    {
        if (damageable)
        {
            hp -= DmgAmount;
            damageStartTime = Time.timeSinceLevelLoad;
        }
    }

    public void Blow(Vector2 force)
    {
        rb.AddForce(force * blowForceInfluence);
    }

    public void Stick(float strength)
    {
        rb.drag += strength;
    }

    public void Pop()
    {
        int bubbleSpawnAmt = 3;

        //play pop particle effect
        GameObject popParticle2 = Instantiate(popParticle);
        ParticleSystem particle = popParticle2.GetComponent<ParticleSystem>();
        popParticle2.transform.position = gameObject.transform.position;
        particle.Play();

        //spawn baby bubbles
        if (spawnBubblesOnDeath)
        {
            for (int i = 0; i < bubbleSpawnAmt; i++)
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

    private void OnDrawGizmos()
    {
        foreach(Vector2 dir in raycastDirections)
        {
            Gizmos.color = validDirections[raycastDirections.IndexOf(dir)] ? Color.green : Color.red;
            Gizmos.DrawRay(new Ray(transform.position, dir.normalized));
        }
    }

    private void CheckStuck()
    {

        float stuckSpeed = 0.01f;

        //get current speed
        speed = Vector3.Distance(oldPosition, transform.position); //This is the speed per frames
        //speedPerSecond = Vector3.Distance(oldPosition, transform.position) / Time.deltaTime; //This is the speed per second
        oldPosition = transform.position;
        //Debug.Log("Speed: " + speed.ToString("F2"));

        // if stuck for longer than 5 seconds, stuck = true
        if(speed > stuckSpeed)
        {
            lastTime = Time.timeSinceLevelLoad;
        }
        if (Time.timeSinceLevelLoad > stuckTime + lastTime)
        {
            Debug.Log("bubble is stuck");
            Stuck = true;
        }
    }
}
