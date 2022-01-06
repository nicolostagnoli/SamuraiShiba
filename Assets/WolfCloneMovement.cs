using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfCloneMovement : MonoBehaviour
{
    private GameObject _shiba;
    private float _movementSpeed = 2f; 
    // Start is called before the first frame update
    void Start()
    {
        _shiba = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (_shiba != null)
        {
            ChaseShiba();
        }
    }

    void ChaseShiba()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, _shiba.transform.position, _movementSpeed * Time.deltaTime);
        
         if (transform.position.x < _shiba.transform.position.x)
         {
             TurnRight();
         }
        else
         {
             TurnLeft();
         }
        
    }

    void TurnRight()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void TurnLeft()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}