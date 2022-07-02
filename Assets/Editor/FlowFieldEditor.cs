using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FlowField))]
public class FlowFieldEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var flowField = target as FlowField;

        if (GUILayout.Button("Create Field"))
        {
            var field = new Texture2D(flowField.cells.x, flowField.cells.y, TextureFormat.RFloat, false, true);

            for (int y = 0; y < field.height; y++)
            {
                for (int x = 0; x < field.width; x++)
                {
                    field.SetPixel(x, y, new Color(1, 0, 0));
                }
            }

            field.Apply();

            flowField.field = field;
        }

        if (GUILayout.Button("Carve"))
        {
            var field = flowField.field;

            for (int x = 0; x < field.width; x++)
            {
                for (int y = 0; y < field.height; y++)
                {
                    var center = flowField.GetCenter(new Vector2Int(x, field.height - y));

                    var ray = new Ray(new Vector3(center.x, center.y, -1), Vector3.forward);
                    var hitInfo = Physics2D.GetRayIntersection(ray, Mathf.Infinity, LayerMask.GetMask("Obstacle"));

                    if (hitInfo)
                    {
                        field.SetPixel(x, y, Color.black);
                    }
                }
            }

            field.Apply();
        }
    }
}
