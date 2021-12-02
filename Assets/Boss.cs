using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
    }

    public float getCurrentHealth() {
        return currentHealth;
    }
}
