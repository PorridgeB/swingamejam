using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<SpawnItem> spawnItems;
    [Tooltip("The time until bubbles should start spawning")]
    public float startDelay;
    [Tooltip("The time in-between spawning the next bubble")]
    public float spawnRate = 0.5f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), startDelay, spawnRate);
    }

    private void Spawn()
    {
        // There are no more bubbles to spawn
        if (spawnItems.Count == 0)
        {
            Destroy(gameObject);

            return;
        }
        
        // Pop the first bubble off of the queue
        var spawnPrefab = spawnItems[0].prefab;
        spawnItems.RemoveAt(0);

        var bubble = Instantiate(spawnPrefab);
        bubble.transform.position = transform.position;
    }
}
