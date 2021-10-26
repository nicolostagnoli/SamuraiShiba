using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int _health = PlayerMaxHealth;
    private int _stamina = PlayerMaxStamina;
    private const int PlayerMaxStamina=200;
    private const int PlayerMaxHealth=200;

    private HealthBarScript healthBarScript;
    private StaminaBarScript staminaBarScript;

    private void Start()
    {
        healthBarScript = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
        staminaBarScript=GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<StaminaBarScript>();
        healthBarScript.SetMaxHealth(PlayerMaxHealth);
        staminaBarScript.SetMaxStamina(PlayerMaxStamina);
        Debug.Log(healthBarScript);
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
}
