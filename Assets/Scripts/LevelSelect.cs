using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameSettings gameSettings;

    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private GameObject buttons;

    private void Start()
    {
        foreach (Transform child in buttons.transform)
        {
            Destroy(child.gameObject);
        }

        var levels = Resources.LoadAll<Level>("Levels");

        var count = 1;

        foreach (var level in levels)
        {
            var button = Instantiate(buttonPrefab).GetComponent<Button>();
            button.transform.SetParent(buttons.transform, false);

            button.onClick.AddListener(() => OnButtonClicked(level));

            var buttonLabel = button.GetComponentInChildren<TMP_Text>();
            buttonLabel.text = count++.ToString();
        }
    }

    private void OnButtonClicked(Level level)
    {
        gameSettings.currentLevel = level;

        SceneManager.LoadScene("Game");
    }
}
