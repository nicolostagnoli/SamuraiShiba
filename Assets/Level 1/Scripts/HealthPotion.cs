using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : UsableItem
{
  private float _potionValue=0.5f;
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
    if (_playerStats.getHealth() + _potionValue *StateNameController.playerMaxHealth > _playerStats.GetMaxHealth())
    {
      _playerStats.setHealth(_playerStats.GetMaxHealth());
    }
    else 
      _playerStats.setHealth(_playerStats.getHealth() + _potionValue*StateNameController.playerMaxHealth);
    healingParticle.Play();
    Debug.Log("player health "+_playerStats.getHealth());
  }

  public override bool CanUseItem()
  {
    return _playerStats.getHealth() != _playerStats.GetMaxHealth();
  }
  
}
