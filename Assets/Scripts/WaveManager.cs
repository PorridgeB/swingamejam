using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPool;
    [SerializeField] private int spawnCount;

    [SerializeField] private Collider2D spawnField;

    [SerializeField] private Level level;

    [SerializeField] private List<GameObject> wave;

    public bool WaveComplete => wave.Count == 0;

    public void SpawnRandom()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Bubble bubble = Instantiate(spawnPool[Random.Range(0, spawnPool.Count)]).GetComponent<Bubble>();
            bubble.transform.position = spawnField.bounds.center + new Vector3(0, Random.Range(-spawnField.bounds.extents.y, spawnField.bounds.extents.y));
            wave.Add(bubble.gameObject);
        }
    }

    public void Spawn(int round)
    {
        foreach (char c in level.waves[round])
        {
            GameObject go = null;
            switch (c)
            {
                case 's':
                    go = spawnPool[0];
                    break;
                case 'b':
                    go = spawnPool[1];
                    break;
            }

            Bubble bubble = Instantiate(go.GetComponent<Bubble>());
            bubble.transform.position = spawnField.bounds.center + new Vector3(0, Random.Range(-spawnField.bounds.extents.y, spawnField.bounds.extents.y));
            wave.Add(bubble.gameObject);
        }
    }

    private void Update()
    {
        wave.RemoveAll(x => x == null);
    }
}
