using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenEffect : Effect
{
    private void Start()
    {
        bubble.overrideSteering = true;
        bubble.Freeze();
    }

    private void OnDestroy()
    {
        bubble.overrideSteering = false;
    }

    //private void FixedUpdate()
    //{
    //    bubble.Freeze();
    //}
}
