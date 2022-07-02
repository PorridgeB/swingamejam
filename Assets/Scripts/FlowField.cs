using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField : MonoBehaviour
{
    public Vector2 size = new Vector2(10, 10);
    [Tooltip("The number of cells per unit")]
    [Range(0, 2)]
    public float cellDensity = 0.75f;
    [HideInInspector]
    public float[] field;

    public Vector2Int cells => Vector2Int.CeilToInt(size * cellDensity);
    public Vector2 cellSize => size / cells;

    private void Awake()
    {
        field = new float[cells.x * cells.y];

        for (int x = 0; x < cells.x; x++)
        {
            for (int y = 0; y < cells.y; y++)
            {
                field[x + cells.x * y] = Random.Range(0, 360);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        var arrowLength = 0.5f;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, size);

        for (int x = 0; x < cells.x; x++)
        {
            for (int y = 0; y < cells.y; y++)
            {
                var angle = field[x + cells.x * y];
                //var angle = Random.Range(0, 360);
                var direction = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);

                Gizmos.color = Color.HSVToRGB(angle / 360f, 1, 1);

                var center = (Vector2)transform.position - size / 2 + (cellSize * new Vector2(x, y)) + cellSize / 2;

                Gizmos.DrawWireCube(center, cellSize);

                Gizmos.DrawLine(center - direction * arrowLength / 2, center + direction * arrowLength / 2);
            }
        }
    }
}
