using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{

    [SerializeField]
    private Transform _player;
    [SerializeField]
    private float _agroRange;
    [SerializeField]
    private float _moveSpeed;
    public float initialHealth;
    public int damage;
    public LayerMask player;
    private Rigidbody2D _rigidbody;
    private float timeToAttack;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        SetHealth(initialHealth);
        timeToAttack = 0;
    }

    // Update is called once per frame
    void Update() {
        base.Update();

        if (_player != null) {
            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
            if (distanceToPlayer < _agroRange) {
                if (transform.position.x < _player.position.x) {
                    transform.position = Vector2.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
                    TurnRight();

                }
                else if (transform.position.x > _player.position.x) {
                    transform.position = Vector2.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
                    TurnLeft();
                }
            }
        }

        if (timeToAttack > 1.5) {
            Collider2D[] c = Physics2D.OverlapCircleAll(transform.position, 0.2f, player.value);
            if (c.Length > 0) {
                if (c[0].gameObject.GetComponent<PlayerStats>()) {
                    c[0].gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
                    timeToAttack = 0;
                }
            }
        }

        timeToAttack += Time.deltaTime;
    }

    private void TurnRight() {
        transform.localScale = new Vector2(-1, 1);
    }

    private void TurnLeft() {
        transform.localScale = new Vector2(1, 1);
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _agroRange);
    }
}
