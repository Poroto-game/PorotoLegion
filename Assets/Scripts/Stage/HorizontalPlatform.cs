using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    public float _speed;        //Platform speed
    public int startingPoint;   //Starting index
    public Transform[] points;  //Array of transform points

    private int i; //array index


    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //check distance between platform and target
        if(Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if(i == points.Length)
            {
                i = 0;
            }
        }

        //move platform to point index i
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, _speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // if(collision.gameObject.tag == "Player")
        //{
        collision.transform.SetParent(transform);
        //}

        //new GameObject _Collider = collision.transform.GetComponent<Collider>();
        //Debug.Log(collision.transform.tag);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
