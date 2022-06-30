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
    [HideInInspector]
    public bool completed;

    private Queue<SpawnItem> spawnItemQueue;

    public void Begin()
    {
        completed = false;

        spawnItemQueue = new Queue<SpawnItem>(spawnItems);

        CancelInvoke(nameof(Spawn));
        InvokeRepeating(nameof(Spawn), startDelay, spawnRate);
    }

    public void End()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        // There are no more bubbles to spawn
        if (spawnItemQueue.Count == 0)
        {
            completed = true;

            End();

            return;
        }

        // Pop the first bubble off of the queue
        var spawnItem = spawnItemQueue.Dequeue();

        var bubble = Instantiate(spawnItem.prefab);
        bubble.transform.position = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
