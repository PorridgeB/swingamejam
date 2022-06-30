using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPool;
    [SerializeField] private int spawnCount;
    [SerializeField] private Level level;

    public bool WaveComplete;

    private void Start()
    {
        WaveComplete = false;
    }

    public SpawnArea GetSpawnArea()
    {
        var spawnAreas = GameObject.FindGameObjectsWithTag("SpawnArea");

        if (spawnAreas.Length == 0)
        {
            Debug.LogError("No spawn areas");
            return null;
        }

        return spawnAreas[0].GetComponent<SpawnArea>();
    }

    public void SpawnRandom()
    {
        var spawnArea = GetSpawnArea();

        for (int i = 0; i < spawnCount; i++)
        {
            var bubble = Instantiate(spawnPool[Random.Range(0, spawnPool.Count)]).GetComponent<Bubble>();

            bubble.transform.position = spawnArea.RandomPoint();
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

            var bubble = Instantiate(go.GetComponent<Bubble>());

            bubble.transform.position = spawnArea.RandomPoint();
        }
    }

    public void Begin()
    {
        WaveComplete = false;

        // Start all spawners
        foreach (var spawner in FindObjectsOfType<Spawner>())
        {
            spawner.Begin();
        }
    }

    public void CheckForWaveComplete()
    {
        WaveComplete = !(FindObjectsOfType<Bubble>().Any(x => !x.Stuck)
            || FindObjectsOfType<Spawner>().Any(x => !x.completed));

        if (WaveComplete)
        {
            Debug.Log("wave complete");
        }

        ////should only check during action phase

        //// checks current amount of bubbles in scene
        //GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        //if (bubbles.Length == 0)
        //{
        //    Debug.Log(bubbles.Length);
        //    WaveComplete = true;
        //}
        //// checks each bubble to see if they are stuck,
        //// and if all bubbles are stuck consider the level done
        //int stuckBubbles = 0;
        //foreach (GameObject bubble in bubbles)
        //{
        //    Bubble script = bubble.GetComponent<Bubble>();
        //    if (!script.Stuck)
        //    {
        //        break;
        //    }
        //    else
        //    {
        //        stuckBubbles++;
        //    }
        //}
        //if (stuckBubbles == bubbles.Length)
        //{
        //    WaveComplete = true;
        //}
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
