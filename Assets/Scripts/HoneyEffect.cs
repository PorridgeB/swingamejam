using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyEffect : Effect
{
    private void FixedUpdate()
    {
        bubble.Stick(strength);
    }
}
