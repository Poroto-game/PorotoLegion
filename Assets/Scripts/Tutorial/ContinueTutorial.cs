using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueTutorial : MonoBehaviour
{
    private LevelLoader _levelLoader;

    private void Start()
    {
        
            _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

            if(_levelLoader == null)
            {
                Debug.LogError("Level Loader is NULL");
            }
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
               
        if(other.transform.tag == "Player")
        {
            //SceneManager.LoadScene(2);
            _levelLoader.LoadNextLevel();
            //Debug.Log("Player detected");
        }
        
    }


}
