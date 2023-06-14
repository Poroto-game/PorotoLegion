using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BouncyPlatform : MonoBehaviour
{
    [SerializeField]
    private float _forceMagnitude = 20f;
    [SerializeField]
    private Vector2 _forceDirection;
    [SerializeField]
    private float _platformRotationZDegrees;
    [SerializeField]
    private Rigidbody2D _rigidBody;
    private Player _player;
    private AudioSource _audioSource;
    public MMFeedbacks platformWiggle;

    // Start is called before the first frame update
    void Start()
    {
        //define direction of force based on sine and cosine of z rotation of platform using Euler values
        //change angles from degrees to radians using Mathf.Deg2Rad
        //Calculate sine and cosine of direction
        _platformRotationZDegrees = transform.eulerAngles.z;
        _forceDirection.x = Mathf.Sin(Mathf.Deg2Rad * _platformRotationZDegrees) * -1;
        _forceDirection.y = Mathf.Cos(Mathf.Deg2Rad * _platformRotationZDegrees);
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

    }

    //When collision, add force to object in the direction defined, and based on a specified magnitued (need to define magnitude of force for each circumstance)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, _forceMagnitude);
            _player.Jump(0);
            Gamepad.current.SetMotorSpeeds(0.7f, 0f);
            StartCoroutine(ResetRumble(0.2f));
        }

            _rigidBody = collision.rigidbody;
            _rigidBody.AddForce(_forceDirection * _forceMagnitude, ForceMode2D.Impulse);
            _audioSource.Play();
            platformWiggle?.PlayFeedbacks();
        
    }

    IEnumerator ResetRumble(float duration)
    {
        yield return new WaitForSeconds(duration);
        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }
}
