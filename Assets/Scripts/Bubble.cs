using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float steeringForce;
    [SerializeField] private Transform target;
    [SerializeField] private Gradient bubbleColor;
    [SerializeField] private LayerMask steeringMask;
    [SerializeField] private List<Vector2> raycastDirections;
    [SerializeField] private List<bool> validDirections;
    [SerializeField] private float seperationStrength;


    [SerializeField] private Vector2 TargetDirection => (target.position - transform.position).normalized;

    private Vector2 currentDir;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        WindController.blow += Move;

        target = GameObject.Find("Base").transform;
        currentDir = TargetDirection;

        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = bubbleColor.Evaluate(Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(FindDesiredDirection());
        SpreadOut();
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

    void Move(Vector3 force)
    {
        transform.position += force;
    }

    private void OnDrawGizmos()
    {
        foreach(Vector2 dir in raycastDirections)
        {
            Gizmos.color = validDirections[raycastDirections.IndexOf(dir)] ? Color.green : Color.red;
            Gizmos.DrawRay(new Ray(transform.position, dir.normalized));
        }
    }
}
