using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IceFlower : MonoBehaviour
{
    public UnityEvent onFreeze;

    public float maxStartDelay = 0.5f;
    public float rate = 5;
    public float duration = 0.5f;

    [SerializeField]
    private CircleCollider2D effectAreaCollider;
    private float maxRadius;

    private void Start()
    {
        maxRadius = effectAreaCollider.radius;

        GameManager.instance.onFightStart.AddListener(() => { InvokeRepeating(nameof(Freeze), Random.Range(0, maxStartDelay), rate); effectAreaCollider.enabled = false; });
        GameManager.instance.onBuildStart.AddListener(() => CancelInvoke(nameof(Freeze)));
    }

    private void Freeze()
    {
        // Animate the radius of the effect area
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, radius => effectAreaCollider.radius = radius, 0, maxRadius, duration)
            .setOnStart(() => effectAreaCollider.enabled = true)
            .setOnComplete(() => effectAreaCollider.enabled = false);
        
        onFreeze.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, effectAreaCollider.radius);
    }
}
