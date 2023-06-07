using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _ballSpeed = 10.0f;
    private Rigidbody2D _rigid;
    public float _timer;
    [SerializeField]
    private float _selfDestructTime = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _rigid.velocity = transform.forward * _ballSpeed; //generally right instead of forward, but character is rotated by 90 degrees
        _selfDestructTime = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_rigid.position.y < -20)
        {
            Destroy(this.gameObject);
        }

        _timer += Time.deltaTime;

        if (_timer > _selfDestructTime)
        {
            Destroy(this.gameObject, 0.1f);
            _timer = 0.0f;
        }



    }

}
