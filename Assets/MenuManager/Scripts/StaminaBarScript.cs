using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.U))
        {
            ReduceStamina(19);
        }
    }

    public void SetStamina(int stamina)
    {
        slider.value = stamina;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    public void ReduceStamina(int stamina)
    {
        slider.value -= stamina;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int maxStamina)
    {
        slider.maxValue = maxStamina;
        slider.value = maxStamina;
        
        fill.color = gradient.Evaluate(1f);
    }
}


