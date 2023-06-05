using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCeiling : MonoBehaviour
{
    
    public float xStartingPosition;
    public float yStartingPosition;
    public Vector3 resetPosition;
    private AudioSource _audioSource;

    void Start()
    {
        resetPosition = new Vector3(xStartingPosition, yStartingPosition, 0f);
        _audioSource = GameObject.Find("Player Ceiling").GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.parent.position = resetPosition;
            _audioSource.Play();
        }

    }
}
