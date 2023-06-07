using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject _ballPrefab;
    [SerializeField]
    private AudioClip _ballPop;
    [SerializeField]
    private AudioSource _audioSource;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _gunExplosionFX;
    private AudioSource _BallSpawnPoint;
    //Trajectory variables
    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    public Vector2 direction;
    public float ballForce = 20f;
    private GameManager _gameManager;


    void Start()
    {
        _BallSpawnPoint = GameObject.Find("BallSpawnPoint").GetComponent<AudioSource>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _audioSource = _BallSpawnPoint.GetComponent<AudioSource>();
        //_player = GameObject.Find("BallSpawnPoint").GetComponent<Transform>();

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is null");
        }
        else
        {
            _audioSource.clip = _ballPop;
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        //Trajectory
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, firePoint.position, Quaternion.identity);
            points[i].transform.SetParent(firePoint);
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick1Button5)) && GameManager.ammo > 0)
        {
            Shoot();
        }
        direction = new Vector2(firePoint.transform.rotation.y, firePoint.transform.rotation.x);

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }
    }



    void Shoot()
    {
        //if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick1Button5)) && GameManager.ammo > 0)
        
    //Debug.Log(firePoint.rotation);
    Instantiate(_ballPrefab, firePoint.position, firePoint.rotation);
    _audioSource.Play();
    _gameManager.MinusAmmo(1);
    Instantiate(_gunExplosionFX,firePoint.position, firePoint.rotation);
    Gamepad.current.SetMotorSpeeds(0.7f, 0.7f);
    StartCoroutine(ResetRumble(0.1f));
    }

    Vector2 PointPosition(float time)
    {
        Vector2 position = (Vector2)firePoint.position + (direction * ballForce * time) + 0.3f * Physics2D.gravity * (time * time);
        return position;
    }

    IEnumerator ResetRumble(float duration)
    {
        yield return new WaitForSeconds(duration);
        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }

}
