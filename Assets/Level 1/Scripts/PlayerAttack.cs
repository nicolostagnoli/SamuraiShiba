using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttacks;
    private Animator _animator;

    public float startTimeBetweenAttacks;
    public Transform attackPosition;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public float damage;

    // Start is called before the first frame update
    private void Start() {
        timeBetweenAttacks = 0;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(timeBetweenAttacks <= 0) {

           if (Input.GetKey(KeyCode.P)) {
                //Debug.Log("Attacking");
                Collider2D[] enemiesToAttack = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemy);
                for(int i = 0; i < enemiesToAttack.Length; i++) {
                    enemiesToAttack[i].gameObject.GetComponent<EnemyScript>().TakeDamage(damage);
                    Debug.Log("Damage to: " + enemiesToAttack[i].gameObject.name);
                }
                timeBetweenAttacks = startTimeBetweenAttacks;

                _animator.SetTrigger("Attack");
            }
        }
        else {
            timeBetweenAttacks -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
