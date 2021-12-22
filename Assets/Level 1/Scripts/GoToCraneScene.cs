using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToCraneScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.Equals("Shiba"))
        {
            StateNameController.playerHealth =
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().getHealth();
            StateNameController.playerStamina =
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().getStamina();
            StateNameController.playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            StateNameController.playerInventory.setSlots(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().slots);
            SceneManager.LoadScene("Level 1/Scenes/Crane");
        }
    }
}
