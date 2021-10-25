using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetMaxHealth(100);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TakeDamage(19);
        }
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    public void TakeDamage(int health)
    {
        slider.value -= health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        
        fill.color = gradient.Evaluate(1f);
    }
}
