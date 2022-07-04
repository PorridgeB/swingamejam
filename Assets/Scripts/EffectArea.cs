using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EffectArea : MonoBehaviour
{
    public UnityEvent onBubbleEffected;

    public EffectType effectType;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var bubble = collision.GetComponent<Bubble>();

        if (bubble == null)
        {
            return;
        }

        var effects = bubble.GetComponentsInChildren<Effect>();
        var existingEffect = effects.FirstOrDefault(x => x.effectType == effectType);

        if (existingEffect == null)
        {
            Instantiate(effectType.prefab, bubble.transform);

            onBubbleEffected.Invoke();
        }
        else
        {
            existingEffect.Reset();
        }
    }
}
