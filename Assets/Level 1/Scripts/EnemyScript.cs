using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    
    [SerializeField]
    private float _agroRange;
    
    [SerializeField]
    private float _moveSpeed;

    public float initialHealth;
    private float health;

    private Vector3 startingPosition;

    private Vector3 roamPosition;

    private Animator _animator;

    
    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        roamPosition = GetRoamingPosition();
        _animator = GetComponent<Animator>();

        health = initialHealth;
    }

    void Update()
    {
        //die
        if (health <= 0)
        {
            _animator.SetTrigger("Die");
        }
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
            turnRight();
        }
        else if(transform.position.x > roamPosition.x)
        {
            turnLeft();
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
            turnRight();

        }
        else if(transform.position.x > _player.position.x)
        {
            _rigidbody.velocity = new Vector2(-_moveSpeed, 0);
            turnLeft();
        }
    }

    private void turnRight()
    {
        transform.localScale = new Vector2(1, 1);

    }

    private void turnLeft()
    {
        transform.localScale = new Vector2(-1, 1);

    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0, 0)).normalized* Random.Range( 4f,4f);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        _animator.SetTrigger("Hit");
    }

    public void OnCompleteDieAnimation(){
        Destroy(gameObject);
    }
}
