using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToCraneScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.Equals("Shiba")) {
            SceneManager.LoadScene("Level 1/Scenes/Crane");
        }
    }
}
