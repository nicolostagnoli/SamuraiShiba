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
            StateNameController.playerHealth =
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().getHealth();
            StateNameController.playerStamina =
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().getStamina();
            StateNameController.playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponents<Inventory>()[0];
            Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>());
            StateNameController.playerInventory.setSlots(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().slots);
            SceneManager.LoadScene("Level 2/Scenes/Level2");
        }
    }
}
