using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFlower : MonoBehaviour
{
    public float freezeRate = 5;

    [SerializeField]
    private GameObject effectArea;

    private void Start()
    {
        effectArea.SetActive(false);

        InvokeRepeating(nameof(Freeze), 0, freezeRate);
    }

    private void Freeze()
    {
        StartCoroutine(FlickerEffectArea());
    }

    private IEnumerator FlickerEffectArea()
    {
        effectArea.SetActive(true);
        //yield return null;
        yield return new WaitForSeconds(0.1f);
        effectArea.SetActive(false);
    }
}
