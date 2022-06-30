using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState
{
    Build,
    Fight
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Inventory inventory;
    public UnityEvent onBuildStart;
    public UnityEvent onFightStart;

    [SerializeField] private GameState state;
    [SerializeField] private int stage;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameObject buildUI;
    [SerializeField]
    private HealthBar baseHealthBar;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        state = GameState.Build;
        stage = 1;

        // Load level
        SceneManager.LoadScene(SceneLoaderScript.levelSceneToLoad, LoadSceneMode.Additive);
    }

    public void ChangeGameState()
    {
        switch (state)
        {
            case GameState.Build:
                //waveManager.Spawn(stage-1);
                waveManager.SpawnRandom();
                buildUI.SetActive(false);
                state = GameState.Fight;
                waveManager.WaveComplete = false;
                onFightStart.Invoke();
                break;
            case GameState.Fight:
                stage++;
                buildUI.SetActive(true);
                state = GameState.Build;
                onBuildStart.Invoke();
                waveManager.ClearBubbles();
                break;
        }
    }

    private void Update()
    {
        if(state == GameState.Fight)
        {
            waveManager.CheckForWaveComplete();
        }

        if (waveManager.WaveComplete && state == GameState.Fight)
        {
            Debug.Log(waveManager.WaveComplete);
            Debug.Log("Wave complete.");
            ChangeGameState();
        }

        // Quick way to link the base health bar to the level's base
        var hill = FindObjectOfType<Base>();
        if (hill != null)
        {
            baseHealthBar.health = hill.GetComponent<Health>();
        }
    }
}
