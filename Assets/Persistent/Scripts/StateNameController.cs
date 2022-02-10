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

    public static void SetEasyDifficulty()
    {
        playerMaxHealth = 200;
        healthRegeneration = 3;
    }
    
    public static void SetMediumDifficulty()
    {
        playerMaxHealth = 110;
        healthRegeneration = 1.5f;
    }
    
    public static void SetHardDifficulty()
    {
        playerMaxHealth = 70;
        healthRegeneration = 1;
    }
    
    
}
