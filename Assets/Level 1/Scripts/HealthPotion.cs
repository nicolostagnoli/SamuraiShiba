using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : UsableItem
{
  private int _potionValue=20;
  private PlayerStats _playerStats;
  private void Start()
  {
    _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    InitCategory();
    SetItemName(ItemName.HealthPotion);
  }

  public override void UseItem()
  {
    _playerStats.setHealth(_playerStats.getHealth() + _potionValue);
  }
  
}
