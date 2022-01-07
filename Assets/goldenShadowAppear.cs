using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class goldenShadowAppear : MonoBehaviour
{
    public bool is_colliding = false;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        is_colliding = true;
        Debug.Log("OnCollisionEnter2D");
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        is_colliding = false;
        Debug.Log("OnCollisionEnter2D");
    }
}
