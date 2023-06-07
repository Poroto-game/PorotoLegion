using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PorotoTiles : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
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

            //update UI
            //Call TIleCollected on Player, passing on Tile Tag         
            _gameManager.TileCollected(transform.tag);
            Instantiate(_explosionPrefab, transform.position, transform.rotation);
            Gamepad.current.SetMotorSpeeds(1f, 1f);
            StartCoroutine(ResetRumble(0.1f));
            Destroy(this.gameObject, 0.2f);
            

        }

    }

    IEnumerator ResetRumble(float duration)
    {
        yield return new WaitForSeconds(duration);
        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }


}
