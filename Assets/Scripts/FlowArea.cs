using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlowArea : MonoBehaviour
{
    private const float arrowThickness = 2;
    private const float arrowLength = 2;
    private const float arrowHeadAngle = 45;
    private const float arrowHeadLength = 0.75f;

    [Range(0, 360)]
    public float angle;

    public Vector2 direction => Quaternion.Euler(0, 0, angle) * Vector2.right;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var bubble = collision.GetComponent<Bubble>();

        if (bubble == null)
        {
            return;
        }

        bubble.flowDirection = direction;
    }

    private void OnDrawGizmos()
    {
        var endPoint = transform.position + new Vector3(direction.x, direction.y) * arrowLength;

        Handles.color = Color.blue;
        Handles.DrawLine(transform.position, endPoint, arrowThickness);

        var arrowHeadDirection1 = Quaternion.Euler(0, 0, 180 + arrowHeadAngle) * direction;
        var arrowHeadDirection2 = Quaternion.Euler(0, 0, 180 - arrowHeadAngle) * direction;

        Handles.DrawLine(endPoint, endPoint + arrowHeadDirection1 * arrowHeadLength, arrowThickness);
        Handles.DrawLine(endPoint, endPoint + arrowHeadDirection2 * arrowHeadLength, arrowThickness);

        var boxCollider2D = GetComponent<BoxCollider2D>();

        if (boxCollider2D != null)
        {
            Handles.DrawWireCube(transform.position + new Vector3(boxCollider2D.offset.x, boxCollider2D.offset.y), boxCollider2D.size);
        }
    }
}
