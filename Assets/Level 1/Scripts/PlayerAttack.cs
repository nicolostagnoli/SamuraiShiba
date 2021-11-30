using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float _timeBetweenAttacks;
    private Animator _animator;
    private int _comboCont;
    private float _timeBetweenCombo;
    private PlayerStats _playerStats;

    public float startTimeBetweenLightAttacks;
    public float startTimeBetweenHeavyAttacks;
    public Transform attackPosition;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public float lightDamage;
    public float heavyDamage;
    public float comboDuration;
    public float comboDamageBoost;
    public float lightAttackStamina;
    public float heavyAttackStamina;

    // Start is called before the first frame update
    private void Start() {
        _timeBetweenAttacks = 0;
        _timeBetweenCombo = 0;
        _comboCont = 0;
        _animator = GetComponent<Animator>();
        _playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(_timeBetweenAttacks <= 0) {
            //LIGHT ATTACK
           if (Input.GetKey(KeyCode.N)) {
                if (_playerStats.getStamina() >= lightAttackStamina) {
                    _playerStats.UseStamina((int)lightAttackStamina);
                    _timeBetweenAttacks = startTimeBetweenLightAttacks;
                    int lightDice = Random.Range(0, 2);
                    if (lightDice == 0)
                        _animator.SetTrigger("Light1");
                    else
                        _animator.SetTrigger("Light2");
                }

            } //HEAVY ATTACK
            else if (Input.GetKey(KeyCode.M)) {
                if (_playerStats.getStamina() >= heavyAttackStamina) {
                    _playerStats.UseStamina((int)heavyAttackStamina);
                    _timeBetweenAttacks = startTimeBetweenHeavyAttacks;
                    int heavyDice = Random.Range(0, 2);
                    if( heavyDice == 0)
                        _animator.SetTrigger("Heavy1");
                    else
                        _animator.SetTrigger("Heavy2");
                }
            }
        }
        else {
            _timeBetweenAttacks -= Time.deltaTime;
        }
        _timeBetweenCombo -= Time.deltaTime;
    }

    public void MakeLightAttack() {
        Collider2D[] enemiesToAttack = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemy);
        for (int i = 0; i < enemiesToAttack.Length; i++) {
            enemiesToAttack[i].gameObject.GetComponent<Enemy>().TakeDamage(lightDamage + _comboCont * comboDamageBoost);
            //combo counter
            if (_timeBetweenCombo > 0) {
                _comboCont++;
            }
            else {
                _comboCont = 1;
            }
            Debug.Log(_comboCont + " Hit!");
            _timeBetweenCombo = comboDuration;
        }
    }

    public void MakeHeavyAttack() {
        Collider2D[] enemiesToAttack = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemy);
        for (int i = 0; i < enemiesToAttack.Length; i++) {
            enemiesToAttack[i].gameObject.GetComponent<Enemy>().TakeDamage(heavyDamage + _comboCont * comboDamageBoost * 1.2f);
            //Combo counter
            if (_timeBetweenCombo > 0) {
                _comboCont++;
            }
            else {
                _comboCont = 1;
            }
            Debug.Log(_comboCont + " Hit!");
            _timeBetweenCombo = comboDuration;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
