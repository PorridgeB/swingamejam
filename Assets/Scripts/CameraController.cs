using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float acceleration = 18;
    public float deceleration = 30;
    public float maxSpeed = 40;
    public Rect boundary;

    private Vector2 velocity;

    private void Update()
    {
        UpdateMovement();
        EnforceBoundary();
    }

    private void UpdateMovement()
    {
        var inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        var targetVelocity = inputDirection * maxSpeed;

        var accel = Vector2.Dot(velocity, targetVelocity) > 0 ? acceleration : deceleration;
        velocity = Vector2.Lerp(velocity, targetVelocity, accel * Time.deltaTime);

        transform.position += new Vector3(velocity.x, velocity.y) * Time.deltaTime;
    }

    private void EnforceBoundary()
    {
        var clampedPosition = transform.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, boundary.xMin, boundary.xMax);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, boundary.yMin, boundary.yMax);

        transform.position = clampedPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boundary.center, boundary.size);
    }
}
