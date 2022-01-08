using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float _timeBetweenAttacks;
    private Animator _animator;
    [SerializeField]
    private int _comboContLight;
    [SerializeField]
    private int _comboContHeavy;
    private float _timeBetweenCombo;
    private PlayerStats _playerStats;
    private bool lightAttackEnabled;
    private bool heavyAttackEnabled;

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
        _comboContHeavy = 0;
        _animator = GetComponent<Animator>();
        _playerStats = GetComponent<PlayerStats>();
        lightAttackEnabled = false;
        heavyAttackEnabled = false;
        attackRange = StateNameController.attackRange;
    }

    // Update is called once per frame
    private void Update()
    {
        if(_timeBetweenAttacks <= 0) {
            //LIGHT ATTACK
           if (Input.GetKey(KeyCode.J)) {
                if (_playerStats.getStamina() >= lightAttackStamina) {
                    _playerStats.UseStamina(lightAttackStamina);
                    _timeBetweenAttacks = startTimeBetweenLightAttacks;
                    string attack = ((_comboContLight % 3) + 1).ToString();
                    _animator.SetTrigger("Light" + attack);
                    comboCounter(true);
                }

            } //HEAVY ATTACK
            else if (Input.GetKey(KeyCode.K)) {
                if (_playerStats.getStamina() >= heavyAttackStamina) {
                    _playerStats.UseStamina(heavyAttackStamina);
                    _timeBetweenAttacks = startTimeBetweenHeavyAttacks;
                    string attack = ((_comboContHeavy % 2) + 1).ToString();
                    _animator.SetTrigger("Heavy" + attack);
                    comboCounter(false);
                }
            }
        }
        else {
            _timeBetweenAttacks -= Time.deltaTime;
        }

        _timeBetweenCombo -= Time.deltaTime;
        if(_timeBetweenCombo < 0) {
            _comboContLight = 0;
            _comboContHeavy = 0;
        }

        if (lightAttackEnabled) {
            Collider2D[] enemiesToAttack = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToAttack.Length; i++) {
                enemiesToAttack[i].gameObject.GetComponent<Enemy>().TakeDamage(lightDamage + _comboContHeavy * comboDamageBoost);
                CinemachineShake.Instance.ShakeCamera(0.5f, 0.1f); 
            }
        }

        if (heavyAttackEnabled) {
            Collider2D[] enemiesToAttack = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToAttack.Length; i++) {
                enemiesToAttack[i].gameObject.GetComponent<Enemy>().TakeDamage(heavyDamage + _comboContHeavy * comboDamageBoost);
                CinemachineShake.Instance.ShakeCamera(2f, 0.1f);
            }
        }
    }
    private void comboCounter(bool light) {
        if (_timeBetweenCombo > 0) {
            if (light)
                _comboContLight++;
            else
                _comboContHeavy++;
        }
        else {
            if (light)
                _comboContLight = 1;
            else
                _comboContHeavy = 1;
        }
        _timeBetweenCombo = comboDuration;
    }

    public void EnableLightAttack() {
        lightAttackEnabled = true;
    }

    public void DisableLightAttack() {
        lightAttackEnabled = false;
        heavyAttackEnabled = false;
    }

    public void EnableHeavyAttack() {
        heavyAttackEnabled = true;
    }

    public void DisableHeavyAttack() {
        lightAttackEnabled = false;
        heavyAttackEnabled = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
