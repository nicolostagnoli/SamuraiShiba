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
    

    public void SetHealth(int health)
    {
        slider.value = health;
        Debug.Log("slider value"+slider.value);
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetMaxHealth(int maxHealth)
    {
        
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        Debug.Log("maxvalue: "+maxHealth);
        fill.color = gradient.Evaluate(1f);
    }
}
