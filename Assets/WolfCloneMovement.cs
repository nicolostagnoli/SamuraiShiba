using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfCloneMovement : Enemy
{
    private GameObject _shiba;
    private float _movementSpeed = 1.5f; 
    public  float initialHealth = 1;
    public float damage = 3;
    // Start is called before the first frame update
    void Start()
    {
        _shiba = GameObject.FindWithTag("Player");
        SetHealth(initialHealth);
    }
    // Update is called once per frame
    void Update()
    {
        base.Update();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            if (!other.gameObject.GetComponent<PlayerStats>().GetInvulnerability())
            {
                Destroy(gameObject);
            }
        }
    }
}
