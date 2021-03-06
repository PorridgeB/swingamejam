using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private const float sizePadding = 1.25f;

    public EffectType effectType;
    public float strength = 1;
    public float duration = 1;

    protected Bubble bubble;
    protected SpriteRenderer sprite;
    protected float timer;

    protected virtual void Awake()
    {
        bubble = GetComponentInParent<Bubble>();
        sprite = GetComponent<SpriteRenderer>();

        // Adjust the size of effect overlay to fit the size of the bubble
        var scale = sizePadding * 2 * bubble.radius / sprite.sprite.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, 1);
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;

        if (timer > duration)
        {
            OnExpire();
        }
    }

    protected virtual void OnExpire()
    {
        Destroy(gameObject);
    }

    public void Reset()
    {
        timer = 0;
    }
}
