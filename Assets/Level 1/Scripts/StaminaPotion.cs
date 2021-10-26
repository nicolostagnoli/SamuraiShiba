using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPotion : Item
{
    private int _potionValue=20;
    private PlayerStats _playerStats;
    private void Start()
    {
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    public override void useItem()
    {
        _playerStats.setStamina(_playerStats.getStamina()+_potionValue);
        print("stamina: " +_playerStats.getStamina());
    }
}
