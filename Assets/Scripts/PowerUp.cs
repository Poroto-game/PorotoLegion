using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private int _selectedPowerUp;
    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>(); //Connection to the Player Script
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL");
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            _selectedPowerUp = Random.Range(1, 4);
            //Communicate to player which power up was selected
            ActivatePowerUp(_selectedPowerUp);
            //instantiate FX for destroyed barrel
            Destroy(this.gameObject, 0.1f);
        }
    }

    public void ActivatePowerUp(int powerUpIndex)
    {
        switch (powerUpIndex)
        {
            case 1:
                _gameManager.ActivateHighJump();
                break;
            case 2:
                _gameManager.ActivateMakeItRain();
                break;
            case 3:
                _gameManager.ActivateHighSpeed();
                break;

        }
    }



}
