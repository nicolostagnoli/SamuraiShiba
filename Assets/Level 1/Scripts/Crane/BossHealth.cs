using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Enemy
{
    public float maxHealth;
    public HealthBarScript barScript;

    void Start()
    {
        SetHealth(maxHealth);
        barScript.SetMaxHealth(maxHealth);
    }

    public override void TakeDamage(float damage) {
        if (GetHealth() - damage <= 0) {
            PlayerInterface.Instance.Invoke("Pause", 3f);
        }
        base.TakeDamage(damage);
        barScript.SetHealth(GetHealth() - damage); //boss health bar
    }

    public override void SetHealth(float health) {
        base.SetHealth(health);
        barScript.SetHealth(health);
    }
}
