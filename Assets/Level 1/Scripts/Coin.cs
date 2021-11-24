using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    private void Start()
    {
        SetItemName(ItemName.Coin);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Player")) {
            PlayerStats stats = other.gameObject.GetComponent<PlayerStats>();
            stats.addCoin(1);
            Destroy(gameObject);
        }
    }
}
