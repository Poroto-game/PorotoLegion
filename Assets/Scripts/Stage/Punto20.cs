using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Punto20 : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            _gameManager.AddScore(20); //Reference to the Player script to call AddScore
            Gamepad.current.SetMotorSpeeds(0f, 0.2f);
            StartCoroutine(ResetRumble(0.1f));
            Destroy(this.gameObject, 0.15f);
            Instantiate(_explosionPrefab, transform.position, transform.rotation);
            _gameManager.AddRedBalls(1);


        }

    }

    IEnumerator ResetRumble(float duration)
    {
        yield return new WaitForSeconds(duration);
        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }

}
