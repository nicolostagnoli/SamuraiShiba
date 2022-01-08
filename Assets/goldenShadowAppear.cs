using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class GoldenShadowAppear : MonoBehaviour
{
    public bool is_colliding = false;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        is_colliding = true;
        Debug.Log(collision.transform.name);
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        is_colliding = false;
        Debug.Log("OnCollisionEnter2D");
    }
}
