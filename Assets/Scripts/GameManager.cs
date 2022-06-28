using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Build,
    Fight
}

public class GameManager : MonoBehaviour
{
    public Inventory inventory;
    public List<InventoryItem> startingItems;

    [SerializeField] private GameState state;
    [SerializeField] private int stage;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameObject buildUI;

    private void Start()
    {
        foreach (var startingItem in startingItems)
        {
            inventory.AddItem(startingItem);
        }
        state = GameState.Build;
    }

    public void ChangeGameState()
    {
        switch (state)
        {
            case GameState.Build:
                waveManager.Spawn();
                buildUI.SetActive(false);
                state = GameState.Fight;
                break;
            case GameState.Fight:
                buildUI.SetActive(true);
                state = GameState.Build;
                break;
        }
            
    }

    private void Update()
    {
        if (waveManager.WaveComplete && state == GameState.Fight)
        {
            ChangeGameState();
        }
    }
}
