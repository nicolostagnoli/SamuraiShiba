using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : UsableItem
{
  public int _potionValue=20;
  private PlayerStats _playerStats;
  private ParticleSystem healingParticle;
  private void Start()
  {
    _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    healingParticle = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<ParticleSystem>()[1];
    InitCategory();
    SetItemName(ItemName.HealthPotion);
  }

  public override void UseItem()
  {
    if (_playerStats.getHealth() + _potionValue > _playerStats.GetMaxHealth())
    {
      _playerStats.setHealth(_playerStats.GetMaxHealth());
    }
    else 
      _playerStats.setHealth(_playerStats.getHealth() + _potionValue);
    healingParticle.Play();
  }

  public override bool CanUseItem()
  {
    return _playerStats.getHealth() != _playerStats.GetMaxHealth();
  }
  
}
