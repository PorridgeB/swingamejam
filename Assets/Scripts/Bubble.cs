using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float steeringForce;
    [SerializeField] private Transform target;
    [SerializeField] private int hp;
    // need to add iframe timer

    // Start is called before the first frame update
    void Start()
    {
        WindController.blow += Move;

        target = GameObject.Find("Base").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }

        transform.position += (target.position - transform.position).normalized *steeringForce;
    }

    void Move(Vector3 force)
    {
        transform.position += force;
    }

    public void TakeDamage(int DmgAmount)
    {
        Debug.Log("Hp: " + hp);
        hp -= DmgAmount;
    }
}
