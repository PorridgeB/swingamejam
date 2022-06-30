using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadTestLevel()
    {
        SceneManager.LoadScene("Game");
    }

    public void loadLvlSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void loadLvl1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void loadLvl2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void loadLvl3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void loadLvl4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void loadLvl5()
    {
        SceneManager.LoadScene("Level5");
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
