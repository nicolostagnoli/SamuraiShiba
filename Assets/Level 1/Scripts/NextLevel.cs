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
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().setSlots(StateNameController.playerInventory.slots);
            StateNameController.playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            StateNameController.playerInventory.setSlots(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().slots);
            SceneManager.LoadScene(scene);
        }
    }
}