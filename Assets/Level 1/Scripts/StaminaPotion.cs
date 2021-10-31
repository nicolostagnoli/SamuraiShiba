using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPotion : UsableItem
{
    private int _potionValue=20;
    private PlayerStats _playerStats;
    private void Start()
    {
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        initCategory();
        setItemName(ItemName.StaminaPotion);
    }
    public override void useItem()
    {
        _playerStats.setStamina(_playerStats.getStamina()+_potionValue);
        Debug.Log("stamina: " +_playerStats.getStamina());
    }
    
}
