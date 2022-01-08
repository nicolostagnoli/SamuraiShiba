using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNameController : MonoBehaviour
{
    public static float playerHealth;
    public static float playerStamina;
    public static float jumpForce = 8f;
    public static Inventory playerInventory= GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    public static float dashSpeed = 3f;
    public static float attackRange = 0.4f;
}
