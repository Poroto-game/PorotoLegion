using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
            Debug.LogError("_player is NULL");
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
            Destroy(this.gameObject, 0.1f);
            

        }

    }
    

}
