using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punto : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private GameObject _explosionPrefab;
    private Player _player;
    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        if(_rigid == null)
        {
            Debug.LogError("_rigid is NULL");
        }

        _player = GameObject.Find("Player").GetComponent<Player>(); //Connection to the Player Script
        if (_player == null)
        {
            Debug.LogError("_player is NULL");
        }

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball" )
        {
            //Add 10 to score
            if(_player != null)
            {
                _gameManager.AddScore(10); 
            }
                        
            Destroy(this.gameObject, 0.1f);
            Instantiate(_explosionPrefab, transform.position, transform.rotation);

        }

    }
}
