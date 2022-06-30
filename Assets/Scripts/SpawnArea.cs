using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public Rect area;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireCube(new Vector2(transform.position.x, transform.position.y) + area.center, area.size);

        Handles.Label(transform.position, "Spawn Area");
    }
#endif

    public Vector2 RandomPoint()
    {
        var randomX = Random.Range(area.xMin, area.xMax);
        var randomY = Random.Range(area.yMin, area.yMax);

        return transform.TransformPoint(new Vector2(randomX, randomY));
    }
}
