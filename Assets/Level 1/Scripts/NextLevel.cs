using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public String scene;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.Equals("Shiba"))
        {
            Inventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            StateNameController.playerHealth =
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().getHealth();
            StateNameController.playerStamina =
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().getStamina();
            StateNameController.playerInventory = playerInventory;
            SceneManager.LoadScene(scene);
        }
    }
}