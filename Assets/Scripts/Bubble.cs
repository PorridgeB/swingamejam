using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private const int obstacleAvoidanceRays = 16;

    [Header("External Influences")]
    [Tooltip("How much blowing forces (e.g Windmill Flower) will affect the movement of the bubble")]
    public float blowingInfluence = 2;
    [Tooltip("How much sticking forces (e.g Honey) will affect the movement of the bubble")]
    public float stickingInfluence = 1;

    [Header("Movement")]
    [Tooltip("The bubble will try and move at this speed")]
    public float targetSpeed = 3;
    [Tooltip("The maximum amount of force that the bubble can use to try and reach its target speed")]
    public float maxForce = 10;
    [Tooltip("How much of the level flow is factored in to the bubble's desired movement direction")]
    public float flowStrength = 3;
    [Tooltip("How much obstacle avoidance is factored in to the bubble's desired movement direction")]
    public float obstacleAvoidanceStrength = 2;
    [Tooltip("The distance from the center of the bubble to detect nearby obstacles")]
    public float obstacleAvoidanceMaxDistance = 2;
    [Tooltip("How much separation between nearby bubbles is factored in to the bubble's desired movement direction")]
    public float separationStrength = 3;
    [Tooltip("The distance from the center of the bubble to find nearby bubbles")]
    public float separationRadius = 1.5f;
    [Space]

    [Tooltip("The amount of damage dealt to the player's ant hill when impacting with it")]
    public float damageOnImpact = 2;

    [SerializeField] private Transform target;
    [SerializeField] private float hp;
    [SerializeField] private bool spawnBubblesOnDeath;
    [SerializeField] private GameObject babyBubblePrefab;
    [SerializeField] private float stuckTime;
    [SerializeField] private GameObject popParticle;

    private bool damageable;
    private float damageStartTime;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask steeringMask;
    [SerializeField] private List<Vector2> raycastDirections;
    [SerializeField] private List<bool> validDirections;

    private bool stuck;
    private Vector2 currentDir;
    private new Rigidbody2D rigidbody;

    private float lastTime;

    public Vector2 flowDirection = Vector3.right;

    private PidController movementForceController;

    private Vector2 TargetDirection => (target.position - transform.position).normalized;

    public bool Stuck { get => stuck; set => stuck = value; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        movementForceController = gameObject.AddComponent<PidController>();
        movementForceController.proportionalCoefficient = 2;
        movementForceController.derivativeCoefficient = 1;
        movementForceController.integralCoefficient = 0.1f;

        damageable = true;
        target = GameObject.Find("Base").transform;
        currentDir = TargetDirection;
        
        Stuck = false;
        stuckTime = 5f;
        lastTime = Time.timeSinceLevelLoad;
    }

    private void FixedUpdate()
    {
        UpdateMovement();

        // On death
        if (hp <= 0)
        {
            Pop();
        }

        // bubble turns red when hit 
        if (damageStartTime != 0)
        {
            float gbColor = (Time.timeSinceLevelLoad - damageStartTime) / 0.5f;

            spriteRenderer.color = new Color(1, gbColor, gbColor, 1);

            if (gbColor > 1)
            {
                damageStartTime = 0;
                spriteRenderer.color = Color.white;
            }
        }

        //checks if the bubble has barely moved for roughly 5 seconds
        CheckStuck();
    }

    private void UpdateMovement()
    {
        var targetDirection = (flowDirection * flowStrength + Separation() * separationStrength + ObstacleAvoidance() * obstacleAvoidanceStrength).normalized;

        var targetVelocity = targetDirection * targetSpeed;

        movementForceController.current = rigidbody.velocity;
        movementForceController.target = targetVelocity;

        rigidbody.AddForce(Vector2.ClampMagnitude(movementForceController.output, maxForce));
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

    private Vector2 ObstacleAvoidance()
    {
        var totalForce = Vector2.zero;

        for (int i = 0; i < obstacleAvoidanceRays; i++)
        {
            var angle = i / (float)obstacleAvoidanceRays * 360;
            var direction = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);

            var hit = Physics2D.Raycast(transform.position, direction, obstacleAvoidanceMaxDistance, LayerMask.GetMask("Obstacle"));

            if (hit)
            {
                totalForce -= direction * (1 - hit.distance / obstacleAvoidanceMaxDistance);
            }
        }

        return totalForce;
    }

    private Vector2 Separation()
    {
        var totalForce = Vector2.zero;

        var overlappingColliders = Physics2D.OverlapCircleAll(transform.position, separationRadius, LayerMask.GetMask("Bubble"));

        foreach (var otherBubble in overlappingColliders)
        {
            if (otherBubble == this)
            {
                continue;
            }

            var deltaPosition = (Vector2)(transform.position - otherBubble.transform.position);
            var distanceFalloff = 1 / (1 + deltaPosition.magnitude);

            totalForce += deltaPosition.normalized * distanceFalloff;
        }

        return totalForce;
    }

    public void Hurt(float amount)
    {
        if (damageable)
        {
            hp -= amount;
            damageStartTime = Time.timeSinceLevelLoad;
        }
    }

    public void Blow(Vector2 force)
    {
        rigidbody.AddForce(force * blowingInfluence);
    }

    public void Stick(float strength)
    {
        rigidbody.AddForce(stickingInfluence * strength * -rigidbody.velocity);
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
                Vector2 pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

                var randomOffset = Random.insideUnitCircle * 0.5f;

                GameObject newBubble = Instantiate(babyBubblePrefab,
                    pos + randomOffset,
                    Quaternion.Euler(0, 0, Random.Range(0, 360)));

                var explosionForce = 1;

                Rigidbody2D rb = newBubble.GetComponent<Rigidbody2D>();
                rb.AddForce(randomOffset.normalized * explosionForce, ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        //foreach (Vector2 dir in raycastDirections)
        //{
        //    Gizmos.color = validDirections[raycastDirections.IndexOf(dir)] ? Color.green : Color.red;
        //    Gizmos.DrawRay(new Ray(transform.position, dir.normalized));
        //}

        var vectorScale = 0.5f;

        var position = new Vector2(transform.position.x, transform.position.y);

        // Flow
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(position, position + flowDirection * flowStrength * vectorScale);

        // Separation
        Gizmos.color = Color.green;
        Gizmos.DrawLine(position, position + Separation() * separationStrength * vectorScale);

        // Obstacle avoidance
        Gizmos.color = Color.red;
        Gizmos.DrawLine(position, position + ObstacleAvoidance() * obstacleAvoidanceStrength * vectorScale);

        // Obstacle avoidance rays
        //var obstacleAvoidanceMaxDistance = 2;
        //var obstacleAvoidanceRays = 16;

        //for (int i = 0; i < obstacleAvoidanceRays; i++)
        //{
        //    var angle = i / (float)obstacleAvoidanceRays * 360;
        //    var direction = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);

        //    Gizmos.DrawLine(position, position + direction * obstacleAvoidanceMaxDistance);
        //}
    }

    private void CheckStuck()
    {
        float stuckSpeed = 0.01f;

        var speed = rigidbody.velocity.magnitude;

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
