using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNameController : MonoBehaviour
{
    public static float playerHealth;
    public static float playerStamina;
    public static float jumpForce = 8f;
    public static Inventory playerInventory;
    public static float dashSpeed = 3f;
    public static float attackRange = 0.4f;
    public static float playerMaxHealth=100;
    public static float healthRegeneration=1.5f;
    public static int HealthPotionQuantity;
    public static int StaminaPotionQuantity;
    public static int ShurikenQuantity;
    public static GameObject HealthPotion;
    public static GameObject StaminaPotion;
    public static GameObject Shuriken;
    public static float lightAttackDamage =8;
    public static float heavyAttackDamage =15 ;

    public static void SetEasyDifficulty()
    {
        playerMaxHealth = 200;
        healthRegeneration = 3;
        lightAttackDamage = 11;
        heavyAttackDamage = 18;
    }
    
    public static void SetMediumDifficulty()
    {
        playerMaxHealth = 100;
        healthRegeneration = 1.5f;
        lightAttackDamage = 9;
        heavyAttackDamage = 16;
    }
    
    public static void SetHardDifficulty()
    {
        playerMaxHealth = 70;
        healthRegeneration = 1;
        lightAttackDamage = 8;
        heavyAttackDamage = 15;
    }

    public static void AddHealPotion(int quantity, GameObject healthPotionButton)
    {
        HealthPotionQuantity += quantity;
        HealthPotion = healthPotionButton;
    }
    
    public static void AddStaminaPotion(int quantity, GameObject staminaPotionButton)
    {
        StaminaPotionQuantity += quantity;
        StaminaPotion = staminaPotionButton;
    }
    
    public static void AddShuriken(int quantity, GameObject shurikenButton)
    {
        ShurikenQuantity += quantity;
        Shuriken = shurikenButton;
    }
    
    
}
