using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneHealth : Enemy
{
    public float maxHealth;
    public HealthBarScript barScript;

    void Start()
    {
        SetHealth(maxHealth);
        barScript.SetMaxHealth(maxHealth);
    }

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);
        barScript.SetHealth(GetHealth() - damage); //boss health bar
    }
}
