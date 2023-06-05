using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _ballSpeed = 10.0f;
    private Rigidbody2D _rigid;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _rigid.velocity = transform.forward * _ballSpeed; //generally right instead of forward, but character is rotated by 90 degrees
    }

    // Update is called once per frame
    void Update()
    {
        if(_rigid.position.y < -20)
        {
            Destroy(this.gameObject);
        }

    }
    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wall" || collision.transform.tag == "Platform")
        {
            _ballSpeed = _ballSpeed * 0.4f;
            _rigid.velocity = transform.forward * _ballSpeed * -1; //generally right instead of forward
           _ballSpeed = _rigid.velocity.x; 
        }

    }*/
}
