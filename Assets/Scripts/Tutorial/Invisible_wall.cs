using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible_wall : MonoBehaviour
{

    //BALL DESTROYER
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            //Debug.Log(collision.transform.tag);
            Destroy(collision.gameObject);
        }
    }
}
