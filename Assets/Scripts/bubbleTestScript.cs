using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleTestScript : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       rb.AddForce(rb.transform.right, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
