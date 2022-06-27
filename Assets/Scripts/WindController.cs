using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float strength;

    public delegate void windBroadcast(Vector3 force);
    public static event windBroadcast blow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        blow?.Invoke(new Vector3(direction.x*strength, direction.y*strength));
    }
}
