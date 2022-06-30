using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    public static string levelSceneToLoad = "LevelTest1";

    public void loadTestLevel()
    {
        LoadLevel("LevelTest1");
    }

    public void loadLvlSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel(string sceneName)
    {
        levelSceneToLoad = sceneName;
        SceneManager.LoadScene("Game");
    }

    public void LoadScene(string s)
    {
        SceneManager.LoadScene(s);
    }

    public void quitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit ();
        #endif
    }
}
