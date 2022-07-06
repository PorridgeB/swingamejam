using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFlower : MonoBehaviour
{
    public float rate = 5;
    public float radius = 5;
    public float speed = 10;

    [SerializeField]
    private EffectType effectType;
    [SerializeField]
    private ParticleSystem freezeParticles;

    private void Start()
    {
        GameManager.instance.onFightStart.AddListener(() => InvokeRepeating(nameof(Freeze), 0, rate));
        GameManager.instance.onBuildStart.AddListener(() => CancelInvoke(nameof(Freeze)));
    }

    private void Freeze()
    {
        freezeParticles.Play();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
