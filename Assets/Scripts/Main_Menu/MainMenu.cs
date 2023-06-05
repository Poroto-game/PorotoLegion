using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private LevelLoader _levelLoader;
    private void Start()
    {
        _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        if (_levelLoader == null)
        {
            Debug.LogError("Level Loader is NULL");
        }

    }

    public void LoadGame()
    {
        GameManager.ammo = 10;
        SceneManager.LoadScene(3);
    }

    public void LoadTutorial()
    {
        _levelLoader.LoadNextLevel();
    }
}
