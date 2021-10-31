using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float _timeBetweenAttacks;
    private Animator _animator;
    private int _comboCont;
    private float _timeBetweenCombo;

    public float startTimeBetweenLightAttacks;
    public float startTimeBetweenHeavyAttacks;
    public Transform attackPosition;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public float lightDamage;
    public float heavyDamage;
    public float comboDuration;
    public float comboDamageBoost;

    // Start is called before the first frame update
    private void Start() {
        _timeBetweenAttacks = 0;
        _timeBetweenCombo = 0;
        _comboCont = 0;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(_timeBetweenAttacks <= 0) {
            //LIGHT ATTACK
           if (Input.GetKey(KeyCode.N)) {
                Debug.Log("Light Attacking");
                Collider2D[] enemiesToAttack = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemy);
                for(int i = 0; i < enemiesToAttack.Length; i++) {
                    enemiesToAttack[i].gameObject.GetComponent<EnemyScript>().TakeDamage(lightDamage+_comboCont*comboDamageBoost);
                    Debug.Log("Damage " + lightDamage+_comboCont*comboDamageBoost + " to: " + enemiesToAttack[i].gameObject.name);
                    //combo counter
                    if(_timeBetweenCombo > 0){
                        _comboCont++;
                    }else{
                        _comboCont = 1;
                    }
                    Debug.Log(_comboCont + " Hit!");
                    _timeBetweenCombo = comboDuration;
                }

                _timeBetweenAttacks = startTimeBetweenLightAttacks;
                _animator.SetTrigger("LightAttack");

            } //HEAVY ATTACK
            else if (Input.GetKey(KeyCode.M)) {
                Debug.Log("Heavy Attacking");
                Collider2D[] enemiesToAttack = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemy);
                for(int i = 0; i < enemiesToAttack.Length; i++) {
                    enemiesToAttack[i].gameObject.GetComponent<EnemyScript>().TakeDamage(heavyDamage+_comboCont*comboDamageBoost*1.2f);
                    Debug.Log("Damage " + heavyDamage+_comboCont*comboDamageBoost*1.2f + " to: " + enemiesToAttack[i].gameObject.name);

                    //Combo counter
                     if(_timeBetweenCombo > 0){
                        _comboCont++;
                    }else{
                        _comboCont = 1;
                    }
                    Debug.Log(_comboCont + " Hit!");
                    _timeBetweenCombo = comboDuration;
                }

                _timeBetweenAttacks = startTimeBetweenHeavyAttacks;
                _animator.SetTrigger("HeavyAttack");
            }
        }
        else {
            _timeBetweenAttacks -= Time.deltaTime;
        }
        _timeBetweenCombo -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
