using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornScript : MonoBehaviour
{
    [SerializeField] public float dmgCooldown;
    [SerializeField]  public float Delay { get; set; }
    private float dmgLastTime;
    private float dmgTimeCooldown;

    // Start is called before the first frame update
    void Start()
    {
        dmgLastTime = 0;
        dmgTimeCooldown = 0;
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

    private void OnTriggerStay2D(Collider2D collider)
    {
        //Debug.Log("Current time: " + Time.timeSinceLevelLoad);
        //Debug.Log("cooldown: " + dmgLastTime + dmgTimeCooldown);

        if (collider.gameObject.CompareTag("Bubble"))
        {
            if (Time.timeSinceLevelLoad > dmgLastTime + dmgTimeCooldown)
            {
                //Debug.Log("bubble takes damage");
                Bubble script = collider.gameObject.GetComponent<Bubble>();
                script.Hurt(1);
                dmgLastTime = Time.timeSinceLevelLoad;
            }
        }
        dmgTimeCooldown = dmgCooldown;
        
    }
}
