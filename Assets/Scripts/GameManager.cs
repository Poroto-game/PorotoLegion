using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

//public class GameManager : MonoBehaviour
public class GameManager: MonoBehaviour
{
    public static bool _isGameOver;
    public GameObject[] _redBallsLeft;
    public static GameObject[] _blueBallsLeft;
    public bool _allTilesCollected;
    public static LevelLoader _levelLoader;
    [SerializeField]
    public static int _score;
    public static int ammo=0;
    public static UIManager _uiManager;
    [SerializeField]
    public int _redBallsDestroyed;
    [SerializeField]
    public bool _isPoCollected;
    [SerializeField]
    public bool _isRoCollected;
    [SerializeField]
    public bool _isToCollected;
    public int ammoPreReset;
    public int _currentScene;
    private Player _player;
    [SerializeField]
    private float _powerUpDuration = 15.0f;
    public GameObject _ballPrefab;
    public Transform[] rainSpawnPoints;
    [SerializeField]
    //private bool _isGamePaused = false;


    public void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>(); //Connection to the UIManager script
        
        if(_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }
        _allTilesCollected = false;
        _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        //ammo = 1000;
        AddAmmo(10);
        ammoPreReset = ammo;
        _currentScene = SceneManager.GetActiveScene().buildIndex;

        if(_currentScene > 0)
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Update()
    {
                
        if(Input.GetKeyDown("r") || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            RestartLevel();
        }

        if (Input.GetKeyDown("m") || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            GoToMainMenu();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (_isGameOver)
        {
            SceneManager.LoadScene(0); //Remember to change it to 0 when done
        }

        _redBallsLeft = GameObject.FindGameObjectsWithTag("Punto20");
        _blueBallsLeft = GameObject.FindGameObjectsWithTag("Ball");

       //Game Over conditions for scenes post-tutorial
        if (_currentScene > 2)
        {
            if (ammo == 0 && _redBallsLeft.Length > 0 && _allTilesCollected && _blueBallsLeft.Length == 0)
            {
                _uiManager.GameOverScreen();
            }

            if (_redBallsLeft.Length == 0)
            {
                _levelLoader.LoadNextLevel();
            }
        }

        if (_currentScene == 2 && _redBallsLeft.Length == 0)
        {
            
            _uiManager.FinishTutorial();
        }

        if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            Time.timeScale = 0;
            _uiManager.PauseMenu(true);
        }


    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void AllTilesCollected()
    {
        _allTilesCollected = true;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScoreUI(_score);
        if(_currentScene > 3)
        _uiManager.AddPointsToCombo(points);
    }

    public void MinusAmmo(int balls)
    {
        ammo -= balls;
        _uiManager.UpdateAmmoUI(ammo);
    }

    public void AddAmmo(int balls)
    {
         ammo += balls;
        _uiManager.UpdateAmmoUI(ammo);
    }

    public void AddRedBalls(int redBalls)
    {
        _redBallsDestroyed = _redBallsDestroyed + redBalls;
    }
    public void TileCollected(string tile)
    {
        
        if (tile == "PO_tile")
        {
            _isPoCollected = true;
        }

        if (tile == "RO_tile")
        {
            _isRoCollected = true;
        }

        if (tile == "TO_tile")
        {
            _isToCollected = true;
        }


        if (_isPoCollected == true && _isRoCollected == true && _isToCollected == true)
        {
            AddAmmo(15); 
            AllTilesCollected(); //evaluate if this could be used elsewhere
        }
    }

    public void ActivateHighJump()
    {
        _player._jumpForce = 25.0f;
        Debug.LogError("High Jump Activated");
        StartCoroutine(CancelHighJump());
    }

    public void ActivateMakeItRain()
    {
        Instantiate(_ballPrefab, rainSpawnPoints[0].transform.position,transform.rotation);
        Instantiate(_ballPrefab, rainSpawnPoints[1].transform.position, transform.rotation);
        Instantiate(_ballPrefab, rainSpawnPoints[2].transform.position, transform.rotation);
        Instantiate(_ballPrefab, rainSpawnPoints[3].transform.position, transform.rotation);
        Debug.LogError("Make It Rain Activated");
    }

    public void ActivateHighSpeed()
    {
        _player.horizontalDampingBasic = 0.3f;
        Debug.LogError("High Speed Activated");
        StartCoroutine(RestoreSpeed());
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _uiManager.PauseMenu(false);
    }

    public void RestartLevel()
    {
        ammo = ammoPreReset - 10;
        _currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_currentScene);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    IEnumerator CancelHighJump()
    {
        yield return new WaitForSeconds(_powerUpDuration);
        _player._jumpForce = 15.0f;
        Debug.LogError("Jump Force Restored");
    }

    IEnumerator RestoreSpeed()
    {
        yield return new WaitForSeconds(_powerUpDuration);
        _player.horizontalDampingBasic = 0.5f;
        Debug.LogError("Speed Restored");
    }



}
