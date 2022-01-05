using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Timers;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class EnemyScript : Enemy
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private float _agroRange;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private float _moveSpeed;
    private float initialHealth=20f;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    public Transform attackPosition;
    public int damage;
    public LayerMask player;
    private float _timer;
    private float _timeBetweenAttacks;
    public float invisibilityDuration = 4f;
    public float invisibilityAttackCooldown = 3f;


    
    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        roamPosition = GetRoamingPosition();
        SetHealth(initialHealth);
        _timeBetweenAttacks = 1;
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
        if (distanceToPlayer < _agroRange)
        {
            ChasePlayer();
            if (distanceToPlayer <= _attackRange)
            {
                StopChasing();
                invisibilityAttackCooldown -= Time.deltaTime;
                Debug.Log("cooldown "+invisibilityAttackCooldown);
                if(invisibilityAttackCooldown<=0)
                {
                    InvisibilityActivated();
                    invisibilityDuration -= Time.deltaTime;
                    Debug.Log("invisibility duration " +invisibilityDuration);
                    if(invisibilityDuration<=0) RandomTeletrasport();

                }
                /*
                if (_timer >_timeBetweenAttacks)
                {
                    AttackPlayer();
                }
                if(_timer < _timeBetweenAttacks+1){
                    _timer += Time.deltaTime;
                }
                */
            }
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
            TurnLeft();

        }
        else if(transform.position.x > _player.position.x)
        {
            _rigidbody.velocity = new Vector2(-_moveSpeed, 0);
            TurnRight();
        }
    }

    private void StopChasing()
    {
        _rigidbody.velocity = Vector2.zero;
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

    private void AttackPlayer()
    {
        Collider2D playerToAttack = Physics2D.OverlapCircle(attackPosition.position, _attackRange, player);
        playerToAttack.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        _timer = 0;
    }

    private void InvisibilityActivated()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        GetComponent<SpriteRenderer>().color = tmp;
        //gameObject.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        // when the invisibility ends
    }

    private void RandomTeletrasport()
    {
        
        //Random position within the player
        ThrowKunai();

    }

    private void ThrowKunai()
    {
        
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        GetComponent<SpriteRenderer>().color = tmp;
        
        GetComponent<BoxCollider2D>().enabled = true;
        invisibilityAttackCooldown = 5f;
    }
    
}
