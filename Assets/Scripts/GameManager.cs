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

    public UnityEvent onBuildStart;
    public UnityEvent onFightStart;

    public GameSettings gameSettings;
    public Inventory inventory;
    public new CameraController camera;

    [SerializeField]
    private GameState state;
    [SerializeField]
    private WaveManager waveManager;
    [SerializeField]
    private GameObject buildUI;
    [SerializeField]
    private HealthBar baseHealthBar;
    [SerializeField]
    private GameObject spawnerPreviewPrefab;

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

        // Load level
        var async = SceneManager.LoadSceneAsync(gameSettings.currentLevel.sceneName, LoadSceneMode.Additive);

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

        // Add starting items to inventory
        foreach (var item in gameSettings.currentLevel.startingInventory)
        {
            inventory.AddItem(item);
        }

        // Create spawner previews
        foreach (var spawner in FindObjectsOfType<Spawner>())
        {
            var spawnerPreview = Instantiate(spawnerPreviewPrefab).GetComponent<SpawnerPreview>();
            spawnerPreview.spawner = spawner;
            spawnerPreview.transform.position = spawner.transform.position;
        }
    }

    public void Cancel()
    {
        foreach (var spawner in FindObjectsOfType<Spawner>())
        {
            spawner.End();
        }

        ChangeGameState();
    }

    public void Quit()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void ChangeGameState()
    {
        switch (state)
        {
            case GameState.Build:
                //waveManager.Spawn(stage-1);
                //waveManager.SpawnRandom();

                waveManager.Begin();

                buildUI.SetActive(false);

                state = GameState.Fight;

                onFightStart.Invoke();

                HideAllSpawnerPreviews();

                // Reset health bar
                baseHealthBar.health.Reset();
                break;
            case GameState.Fight:
                buildUI.SetActive(true);
                state = GameState.Build;
                onBuildStart.Invoke();
                waveManager.ClearBubbles();
                ShowAllSpawnerPreviews();
                break;
        }
    }
    
    private void HideAllSpawnerPreviews()
    {
        foreach (var spawnerPreview in FindObjectsOfType<SpawnerPreview>())
        {
            spawnerPreview.Hide();
        }
    }

    private void ShowAllSpawnerPreviews()
    {
        foreach (var spawnerPreview in FindObjectsOfType<SpawnerPreview>())
        {
            spawnerPreview.Show();
        }
    }

    private void Update()
    {
        if (state == GameState.Fight)
        {
            waveManager.CheckForWaveComplete();
            if(instance.baseHealthBar.health.health <= 0)
            {
                SceneManager.LoadScene("Lose");
            }
        }

        if (waveManager.WaveComplete && state == GameState.Fight)
        {
            Debug.Log(waveManager.WaveComplete);
            Debug.Log("Wave complete.");
            SceneManager.LoadScene("Win");
            //ChangeGameState();
        }
    }
}
