using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Enemy
{
    [SerializeField]
    private Transform _player;
    
    [SerializeField]
    private float _agroRange;
    
    [SerializeField]
    private float _moveSpeed;

    private float initialHealth=20f;

    private Vector3 startingPosition;

    private Vector3 roamPosition;


    
    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        roamPosition = GetRoamingPosition();
        SetHealth(initialHealth);
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
        if (distanceToPlayer < _agroRange)
        {
            ChasePlayer();
        }
        else
        {
            Roam();
        }
    }

    private void Roam()
    {
        float reachedPositionDistance = 1f;
        transform.position = Vector2.MoveTowards(transform.position, roamPosition, 2f* Time.deltaTime);
        if (transform.position.x < roamPosition.x)
        {
            TurnRight();
        }
        else if(transform.position.x > roamPosition.x)
        {
            TurnLeft();
        }
        if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
        {
            roamPosition = GetRoamingPosition();
        }
    }
    
    private void ChasePlayer()
    {
        if (transform.position.x < _player.position.x)
        {
            _rigidbody.velocity = new Vector2(_moveSpeed, 0);
            TurnRight();

        }
        else if(transform.position.x > _player.position.x)
        {
            _rigidbody.velocity = new Vector2(-_moveSpeed, 0);
            TurnLeft();
        }
    }

    private void TurnRight()
    {
        transform.localScale = new Vector2(1, 1);

    }

    private void TurnLeft()
    {
        transform.localScale = new Vector2(-1, 1);

    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0, 0)).normalized* Random.Range( 4f,4f);
    }


}
