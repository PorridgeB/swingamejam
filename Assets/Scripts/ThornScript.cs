using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornScript : MonoBehaviour
{
    [SerializeField] public float dmgCooldown;
    [SerializeField]  public float Delay { get; set; }
    private float dmgLastTime;
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Bubble"))
        {
            Debug.Log("collided with bubble");
            //if(Time.timeSinceLevelLoad > dmgLastTime + dmgCooldown)
            // {
            Bubble script = gameObject.GetComponent<Bubble>();
            script.TakeDamage(1);
            //}
        }
        dmgLastTime = Time.timeSinceLevelLoad;
    }
}
