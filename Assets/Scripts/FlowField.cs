using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField : MonoBehaviour
{
    public Vector2 size = new Vector2(10, 10);
    [Tooltip("The number of cells per unit")]
    [Range(0, 4)]
    public float cellDensity = 0.75f;
    //[HideInInspector]
    //public float[] field;
    [HideInInspector]
    public Texture2D field;
    public Material fieldPreview;

    public Vector2Int cells => Vector2Int.CeilToInt(size * cellDensity);
    public Vector2 cellSize => size / cells;

    private void Awake()
    {
        //field = new float[cells.x * cells.y];

        //for (int x = 0; x < cells.x; x++)
        //{
        //    for (int y = 0; y < cells.y; y++)
        //    {
        //        field[x + cells.x * y] = Random.Range(0, 360);
        //    }
        //}
    }

    private void OnDrawGizmosSelected()
    {
        if (field != null)
        {
            Gizmos.DrawGUITexture(new Rect((Vector2)transform.position - size / 2, size), field);//, fieldPreview);
        }

        //Gizmos.color = Color.cyan;
        Gizmos.color = new Color(0.4f, 0.3f, 0.9f);
        Gizmos.DrawWireCube(transform.position, size);

        for (int x = 0; x < cells.x; x++)
        {
            for (int y = 0; y < cells.y; y++)
            {
                var center = (Vector2)transform.position - size / 2 + (cellSize * new Vector2(x, y)) + cellSize / 2;

                Gizmos.DrawWireCube(center, cellSize);
            }
        }

        var arrowLength = Mathf.Min(cellSize.x, cellSize.y) * 0.5f;

        if (field != null)
        {
            for (int x = 0; x < field.width; x++)
            {
                for (int y = 0; y < field.height; y++)
                {
                    var angle = field.GetPixel(x, y).r * 360;
                    var direction = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);

                    //Gizmos.color = Color.HSVToRGB(angle / 360f, 1, 1);

                    var center = GetCenter(new Vector2Int(x, y));

                    Gizmos.DrawLine(center - direction * arrowLength / 2, center + direction * arrowLength / 2);
                }
            }
        }

        //for (int x = 0; x < cells.x; x++)
        //{
        //    for (int y = 0; y < cells.y; y++)
        //    {
        //        var angle = field[x + cells.x * y];
        //        //var angle = Random.Range(0, 360);
        //        var direction = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);

        //        Gizmos.color = Color.HSVToRGB(angle / 360f, 1, 1);

        //        var center = (Vector2)transform.position - size / 2 + (cellSize * new Vector2(x, y)) + cellSize / 2;

        //        Gizmos.DrawWireCube(center, cellSize);

        //        Gizmos.DrawLine(center - direction * arrowLength / 2, center + direction * arrowLength / 2);
        //    }
        //}
    }

    public Vector2 GetCenter(Vector2Int cellPosition) => (Vector2)transform.position - size / 2 + (cellSize * cellPosition) + cellSize / 2;
}
