using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPool;
    [SerializeField] private int spawnCount;

    [SerializeField] private Collider2D spawnField;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Bubble bubble = Instantiate(spawnPool[Random.Range(0, spawnPool.Count)]).GetComponent<Bubble>();
            bubble.transform.position = spawnField.bounds.center + new Vector3(0, Random.Range(-spawnField.bounds.extents.y, spawnField.bounds.extents.y));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
