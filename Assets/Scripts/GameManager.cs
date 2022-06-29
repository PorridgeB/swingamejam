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
    public Inventory inventory;
    public List<InventoryItem> startingItems;
    public UnityEvent onBuildStart;
    public UnityEvent onFightStart;

    [SerializeField] private GameState state;
    [SerializeField] private int stage;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameObject buildUI;
    [SerializeField]
    private HealthBar baseHealthBar;

    private void Start()
    {
        foreach (var startingItem in startingItems)
        {
            inventory.AddItem(startingItem);
        }

        state = GameState.Build;
        stage = 1;

        // Load level
        SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
    }

    public void ChangeGameState()
    {
        switch (state)
        {
            case GameState.Build:
                waveManager.Spawn(stage-1);
                buildUI.SetActive(false);
                state = GameState.Fight;
                onFightStart.Invoke();
                break;
            case GameState.Fight:
                stage++;
                buildUI.SetActive(true);
                state = GameState.Build;
                onBuildStart.Invoke();
                break;
        }
            
    }

    private void Update()
    {
        if (waveManager.WaveComplete && state == GameState.Fight)
        {
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
