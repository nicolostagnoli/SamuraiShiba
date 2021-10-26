using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item
{
  private int _potionValue=20;
  private PlayerStats _playerStats;

  private void Start()
  {
    _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
  }

  public override void useItem()
  {
    _playerStats.setHealth(_playerStats.getHealth() + _potionValue);
  }
}
