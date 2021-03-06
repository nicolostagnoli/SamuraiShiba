using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private float _agroRange;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private float _moveSpeed;
    public float initialHealth;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    public Transform attackPosition;
    public int damage;
    public LayerMask player;
    private float _timer;
    public float _timeBetweenAttacks;
    private bool inAttackRange;
    public GameObject featherPrefab;



    
    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        roamPosition = GetRoamingPosition();
        SetHealth(initialHealth);
        _timeBetweenAttacks = 0.8f;
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_player != null)
        {
            if (transform.position.x < _player.position.x)
            {
                //transform.position = Vector2.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
                TurnRight();

            }
            else if(transform.position.x > _player.position.x)
            {
                //transform.position = Vector2.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
                TurnLeft();
            }
            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
            if (distanceToPlayer > _agroRange) StopChasing();
            if (distanceToPlayer < _agroRange)
            {
                ChasePlayer();
                if (distanceToPlayer <= _attackRange)
                {
                    inAttackRange = true;
                    StopChasing();
                    if (_timer > _timeBetweenAttacks)
                    {
                        AttackPlayer();
                    }

                    if (_timer < _timeBetweenAttacks + 1)
                    {
                        _timer += Time.deltaTime;
                    }
                }
                else
                {
                    inAttackRange = false;
                }
            }
            else
            {
                Roam();
            }
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
        if (!inAttackRange)
        {
            if (transform.position.x < _player.position.x)
            {
                //transform.position = Vector2.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
                TurnRight();

            }
            else if(transform.position.x > _player.position.x)
            {
                //transform.position = Vector2.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
                TurnLeft();
            }
        }
    }

    private void StopChasing()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    private void TurnRight()
    {
        transform.localScale = new Vector2(-1, 1);

    }

    private void TurnLeft()
    {
        transform.localScale = new Vector2(1, 1);

    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0, 0)).normalized* Random.Range( 2f,2f);
    }

    private void AttackPlayer()
    {
        if (transform.position.x < _player.position.x)
        {
            TurnRight();

        }
        else if(transform.position.x > _player.position.x)
        {
            TurnLeft();
        }
        GameObject feather = Instantiate(featherPrefab,transform.position, Quaternion.identity);
        feather.GetComponent<Feather>().SetDirection(_player.transform.position);
        _timer = 0;
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, _attackRange);
    }
    
}