using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBubble : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float forceAmt;
    // Start is called before the first frame update
    void Start()
    {
        //makes the baby shoot out using its rotation
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * forceAmt, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
