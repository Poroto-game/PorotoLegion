using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Destroy(this.gameObject, 0.1f);
            Instantiate(_explosionPrefab, transform.position, transform.rotation);
            _gameManager.AddRedBalls(1);

        }

    }
}
