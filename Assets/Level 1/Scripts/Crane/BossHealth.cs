using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossHealth : Enemy
{
    public float maxHealth;
    public HealthBarScript barScript;
    public GameObject chest;
    public bool singleDrop;
    public Transform chestSpawnPosition;

    void Start()
    {
        SetHealth(maxHealth);
        barScript.SetMaxHealth(maxHealth);
    }

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);
        barScript.SetHealth(GetHealth());
        if (GetHealth() <= 0)
        {
            PlayerInterface.Instance.Invoke("Pause", 3f);

            if(!singleDrop) dropChest();
        }

        //boss health bar
    }

    public void dropChest()
    {
        Instantiate(chest, chestSpawnPosition.position, Quaternion.identity);
        singleDrop = true;
    }

    public override void SetHealth(float health) {
        base.SetHealth(health);
        barScript.SetHealth(health);
    }
}
