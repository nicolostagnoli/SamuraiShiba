using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenSpiritAppears : MonoBehaviour
{
    public bool isColliding = false;

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Player")
        {
            isColliding = true;
        }

    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            isColliding = false;
        }
    }
}
    
