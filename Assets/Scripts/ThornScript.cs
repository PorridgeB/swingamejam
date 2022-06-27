using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // for iframes either use a dictionary on the thorn script side or
    // use a iframe timer on the bubble script
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            Bubble script = gameObject.GetComponent<Bubble>();
            script.hp -= 1;
        }
    }

}
