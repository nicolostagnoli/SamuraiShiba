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
        InitCategory();
        SetItemName(ItemName.StaminaPotion);
    }
    public override void UseItem()
    {
        _playerStats.setStamina(_playerStats.getStamina()+_potionValue);
        if (_playerStats.getStamina() + _potionValue > _playerStats.GetMaxStamina())
        {
            _playerStats.setStamina(_playerStats.GetMaxStamina());
        }
        Debug.Log("stamina: " +_playerStats.getStamina());
    }
    
    public override bool CannotUseItem()
    {
        return _playerStats.getStamina() == _playerStats.GetMaxStamina();
    }
    
}
