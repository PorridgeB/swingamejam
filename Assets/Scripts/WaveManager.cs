using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPool;
    [SerializeField] private int spawnCount;
    [SerializeField] private Level level;
    [SerializeField] private List<GameObject> wave;

    public bool WaveComplete;

    private void Start()
    {
        WaveComplete = false;
    }

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

    public void CheckForWaveComplete()
    {
        //should only check during action phase

        // checks current amount of bubbles in scene
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        if (bubbles.Length == 0)
        {
            Debug.Log(bubbles.Length);
            WaveComplete = true;
        }
        // checks each bubble to see if they are stuck,
        // and if all bubbles are stuck consider the level done
        int stuckBubbles = 0;
        foreach (GameObject bubble in bubbles)
        {
            Bubble script = bubble.GetComponent<Bubble>();
            if (!script.Stuck)
            {
                break;
            }
            else
            {
                stuckBubbles++;
            }
        }
        if (stuckBubbles == bubbles.Length)
        {
            WaveComplete = true;
        }
    }
    
    public void ClearBubbles()
    {
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach(GameObject bubble in bubbles)
        {
            Destroy(bubble);
        }
    }

}
