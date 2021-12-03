using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float _health = PlayerMaxHealth;
    private float _stamina = PlayerMaxStamina;
    private const float PlayerMaxStamina=100;
    private const float PlayerMaxHealth=100;
    private int _coins=1;

    private HealthBarScript healthBarScript;
    private StaminaBarScript staminaBarScript;
    private CoinCounterTextScript _coinCounterTextScript;

    public float staminaRegenerationSpeed;
    public float invulnerabilityTime;
    private float timeToIvulnerability;

    public GameOverScreen GameOverScreen;

    private void Start()
    {
        healthBarScript = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
        staminaBarScript=GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<StaminaBarScript>();
        //_coinCounterTextScript=GameObject.FindGameObjectWithTag("CoinsAmount").GetComponent<CoinCounterTextScript>();

        
        //_coinCounterTextScript.setCoinsAmount(_coins);
        healthBarScript.SetMaxHealth(PlayerMaxHealth);
        staminaBarScript.SetMaxStamina(PlayerMaxStamina);
        timeToIvulnerability = invulnerabilityTime;
    }

    public void Update() {
        setStamina(getStamina() + staminaRegenerationSpeed * Time.deltaTime);
        timeToIvulnerability += Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        if (timeToIvulnerability > invulnerabilityTime) { //if that time is passed, can take damage
            //_animator.SetTrigger("Hit");
            if (_health > 0) {
                if (_health - damage < 0) setHealth(0);
                setHealth(_health - damage);
            }
            if (_health <= 0) {
                GameOver();
                Destroy(gameObject);
                // _animator.SetTrigger("Die");
            }
            timeToIvulnerability = 0;
        }
    }
    public void UseStamina(float stamina) {
        if (_stamina > 0) {
            if (_stamina - stamina < 0) setStamina(0);
            setStamina(_stamina - stamina);
        }
        if (_stamina <= 0) {

        }
    }

    public bool IsAlive()
    {
        return _health>0;
    }

    public void GameOver()
    {
        GameOverScreen.Setup();
    }

    public void setHealth(float health)
    {
        _health = health;
        healthBarScript.SetHealth(health);
    }
    
    public void setStamina(float stamina)
    {
        _stamina = stamina;
        staminaBarScript.SetStamina(stamina);
    }

    public float getHealth()
    {
        return _health;
    }
    
    public float getStamina()
    {
        return _stamina;
    }

    public float GetMaxHealth()
    {
        return PlayerMaxHealth;
    }
    
    public float GetMaxStamina()
    {
        return PlayerMaxStamina;
    }

    public void addCoin(int amount) {
        _coins+=amount;
        _coinCounterTextScript.setCoinsAmount(_coins);
    }
}
