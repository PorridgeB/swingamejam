using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public float acceleration = 18;
    public float deceleration = 30;
    public float maxSpeed = 40;
    public float minSize = 5;
    public float maxSize = 10;
    public float zoomSensitivity = 0.1f;
    public float zoomSpeed = 10;
    public float initialZoom = 0.5f;
    public Rect boundary;

    private Vector2 velocity;
    private float zoom;
    private float targetZoom;
    private new Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Start()
    {
        zoom = targetZoom = initialZoom;
    }

    private void Update()
    {
        UpdateMovement();
        UpdateZoom();
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
    
    private void UpdateZoom()
    {
        targetZoom = Mathf.Clamp01(targetZoom - Input.mouseScrollDelta.y * zoomSensitivity);

        zoom = Mathf.Lerp(zoom, targetZoom, zoomSpeed * Time.deltaTime);

        camera.orthographicSize = Mathf.Lerp(minSize, maxSize, zoom);
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
