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
    private float initialHealth=2000f;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    public Transform attackPosition;
    public int damage;
    public LayerMask player;
    private float _timer;
    private float _timeBetweenAttacks;
    [SerializeField] float invisibilityDuration = 2f;
    private float invisibilityDurationCooldown;
    public float invisibilityCooldown;
    public float invisibilityAttackCooldown = 2f;
    public GameObject kunai;
    private bool inAttackRange;
    public float teletrasportRange;
    private bool firstTeletrasport;
    public Transform pointA;
    public Transform pointB;
    private bool isInvisible;
    public GameObject canvas;



    
    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        roamPosition = GetRoamingPosition();
        SetHealth(initialHealth);
        _timeBetweenAttacks = 1;
        invisibilityDurationCooldown = invisibilityDuration;
        invisibilityCooldown = invisibilityAttackCooldown;
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
        if (distanceToPlayer < _agroRange)
        {
            //ChasePlayer();
            if (distanceToPlayer <= _attackRange)
            {
                //inAttackRange = true;
                invisibilityAttackCooldown -= Time.deltaTime;
                if(invisibilityAttackCooldown<=0)
                {
                    if(!firstTeletrasport) InvisibilityActivated();


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
            if (isInvisible)
            {
                invisibilityDurationCooldown -= Time.deltaTime;
                if(invisibilityDurationCooldown<=0) RandomTeletrasport(); 
            }
            else
            {
                inAttackRange = false;
            }
        }
        else
        {
            //Roam();
        }
    }

    /*
    private void Roam()
    {
        float reachedPositionDistance = 1f;
        transform.position = Vector2.MoveTowards(transform.position, roamPosition, 2f* Time.deltaTime);
        if (transform.position.x < roamPosition.x)
        {
            TurnLeft();
        }
        else if(transform.position.x > roamPosition.x)
        {
            TurnRight();
        }
        if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
        {
            roamPosition = GetRoamingPosition();
        }
    }
    */
    
    /*
    private void ChasePlayer()
    {
        if (!inAttackRange)
        {
            if (transform.position.x < _player.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
                TurnLeft();

            }
            else if(transform.position.x > _player.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
                TurnRight();
            }
        }
    }
    */

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

    /*
    private void AttackPlayer()
    {
        Collider2D playerToAttack = Physics2D.OverlapCircle(attackPosition.position, _attackRange, player);
        playerToAttack.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        _timer = 0;
    }
    */

    private void InvisibilityActivated()
    {
        isInvisible = true;
        canvas.SetActive(false);
        firstTeletrasport = true;
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        GetComponent<SpriteRenderer>().color = tmp;
        float randomNumber = Random.Range(0f, 1f);
        if(randomNumber>0.5f) transform.position = pointA.position;
        else transform.position = pointB.position;
        //gameObject.SetActive(false);
        //GetComponent<BoxCollider2D>().enabled = false;
        // when the invisibility ends
    }

    private void RandomTeletrasport()
    {
        invisibilityDurationCooldown = invisibilityDuration ;
        //Random position within the player
        float randomNumber = Random.Range(0f, 1f);
        if(randomNumber>0.5f) transform.position = pointA.position;
        else transform.position = pointB.position;
        ThrowKunai();

    }

    private void ThrowKunai()
    {
        
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        GetComponent<SpriteRenderer>().color = tmp;
        isInvisible = false;
        canvas.SetActive(true);
        
        //GetComponent<BoxCollider2D>().enabled = true;
        if (transform.position.x < _player.position.x)
        {
            TurnLeft();

        }
        else if(transform.position.x > _player.position.x)
        {
            TurnRight();
        }
        GameObject feather = Instantiate(kunai,transform.position, Quaternion.identity);
        //da cambiare con il kunai 
        feather.GetComponent<Feather>().SetDirection(_player.transform.position);
        invisibilityAttackCooldown = invisibilityCooldown;
        firstTeletrasport = false;
    }

}
