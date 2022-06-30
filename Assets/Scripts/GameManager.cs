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
    public new CameraController camera;
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
        var async = SceneManager.LoadSceneAsync(SceneLoaderScript.levelSceneToLoad, LoadSceneMode.Additive);

        async.completed += OnLevelLoaded;
    }

    private void OnLevelLoaded(AsyncOperation asyncOperation)
    {
        // Configure hill health bar
        var hill = FindObjectOfType<Base>();
        if (hill != null)
        {
            baseHealthBar.health = hill.GetComponent<Health>();
        }

        // Configure camera bounds
        var levelRect = new Rect();
        levelRect.min = new Vector2(Mathf.Infinity, Mathf.Infinity);
        levelRect.max = new Vector2(Mathf.NegativeInfinity, Mathf.NegativeInfinity);
        
        var gameObjects = FindObjectsOfType<GameObject>();

        foreach (var gameObject in gameObjects)
        {
            if (gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                var collider = gameObject.GetComponent<Collider2D>();

                if (collider != null)
                {
                    levelRect.min = Vector2.Min(levelRect.min, collider.bounds.min);
                    levelRect.max = Vector2.Max(levelRect.max, collider.bounds.max);
                }
            }
        }

        camera.boundary = levelRect;
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
    }
}
