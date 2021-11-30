using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int _health = PlayerMaxHealth;
    private int _stamina = PlayerMaxStamina;
    private const int PlayerMaxStamina=100;
    private const int PlayerMaxHealth=100;
    private int _coins=1;

    private HealthBarScript healthBarScript;
    private StaminaBarScript staminaBarScript;
    private CoinCounterTextScript _coinCounterTextScript;

    private void Start()
    {
        healthBarScript = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
        staminaBarScript=GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<StaminaBarScript>();
        _coinCounterTextScript=GameObject.FindGameObjectWithTag("CoinsAmount").GetComponent<CoinCounterTextScript>();

        
        _coinCounterTextScript.setCoinsAmount(_coins);
        healthBarScript.SetMaxHealth(PlayerMaxHealth);
        staminaBarScript.SetMaxStamina(PlayerMaxStamina);
        Debug.Log(healthBarScript);
    }
    
    public void TakeDamage(int damage)
    {
        //_animator.SetTrigger("Hit");
        if (_health > 0)
        {
            if(_health-damage<0) setHealth(0);
            setHealth(_health-damage);
        }
        if (_health <= 0)
        {
           // _animator.SetTrigger("Die");
        }
    }
    public void UseStamina(int stamina) {
        if (_stamina > 0) {
            if (_stamina - stamina < 0) setStamina(0);
            setStamina(_stamina - stamina);
        }
        if (_stamina <= 0) {

        }
    }


    public void setHealth(int health)
    {
        _health = health;
        healthBarScript.SetHealth(health);
    }
    
    public void setStamina(int stamina)
    {
        _stamina = stamina;
        staminaBarScript.SetStamina(stamina);
    }

    public int getHealth()
    {
        return _health;
    }
    
    public int getStamina()
    {
        return _stamina;
    }

    public int GetMaxHealth()
    {
        return PlayerMaxHealth;
    }
    
    public int GetMaxStamina()
    {
        return PlayerMaxStamina;
    }

    public void addCoin(int amount) {
        _coins++;
        _coinCounterTextScript.setCoinsAmount(_coins);
    }
}
