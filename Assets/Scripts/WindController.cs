using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public static WindController instance;

    public Vector2 direction;
    public float strength;

    public delegate void windBroadcast(Vector3 force);
    public static event windBroadcast blow;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        blow?.Invoke(new Vector3(direction.x*strength, direction.y*strength));
    }
}
