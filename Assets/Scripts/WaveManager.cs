using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPool;
    [SerializeField] private int spawnCount;
    [SerializeField] private Level level;
    [SerializeField] private List<GameObject> wave;

    public bool WaveComplete => wave.Count == 0;

    public BoxCollider2D GetSpawnArea()
    {
        var spawnAreas = GameObject.FindGameObjectsWithTag("SpawnArea");

        if (spawnAreas.Length == 0)
        {
            Debug.LogError("No spawn areas");
            return null;
        }

        return spawnAreas[0].GetComponent<BoxCollider2D>();
    }

    public void SpawnRandom()
    {
        var spawnArea = GetSpawnArea();

        for (int i = 0; i < spawnCount; i++)
        {
            Bubble bubble = Instantiate(spawnPool[Random.Range(0, spawnPool.Count)]).GetComponent<Bubble>();
            bubble.transform.position = spawnArea.bounds.center + new Vector3(0, Random.Range(-spawnArea.bounds.extents.y, spawnArea.bounds.extents.y));
            wave.Add(bubble.gameObject);
        }
    }

    public void Spawn(int round)
    {
        var spawnArea = GetSpawnArea();

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
            bubble.transform.position = spawnArea.bounds.center + new Vector3(0, Random.Range(-spawnArea.bounds.extents.y, spawnArea.bounds.extents.y));
            wave.Add(bubble.gameObject);
        }
    }

    private void Update()
    {
        wave.RemoveAll(x => x == null);
    }
}
