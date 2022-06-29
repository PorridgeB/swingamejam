using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public Rect area;

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireCube(new Vector2(transform.position.x, transform.position.y) + area.center, area.size);

        Handles.Label(transform.position, "Spawn Area");
    }

    public Vector2 RandomPoint()
    {
        var randomX = Random.Range(area.xMin, area.xMax);
        var randomY = Random.Range(area.yMin, area.yMax);

        return transform.TransformPoint(new Vector2(randomX, randomY));
    }
}
