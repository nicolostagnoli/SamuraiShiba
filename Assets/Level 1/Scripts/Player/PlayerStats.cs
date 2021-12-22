using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float PlayerMaxStamina;
    public float PlayerMaxHealth;
    private int _coins=1;
    private float _health;
    private float _stamina;
    private HealthBarScript healthBarScript;
    private StaminaBarScript staminaBarScript;
    private CoinCounterTextScript _coinCounterTextScript;
    private Inventory _inventory;
    private Inventory _playerInventory;
    private const int TotalSlots=4;

    public HitEffect hitEffect;
    public float staminaRegenerationSpeed;
    public float healthRegenerationSpeed;
    public float invulnerabilityTime;
    private float timeToInvulnerability;
    private bool _invulnerable; //Independent from invulnerability time
    
    private void Start()
    {
        healthBarScript = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
        staminaBarScript=GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<StaminaBarScript>();
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if (StateNameController.playerHealth != 0 && StateNameController.playerStamina != 0)
        {
            _health = StateNameController.playerHealth;
            _stamina = StateNameController.playerStamina;
            _inventory=Inventory.CreateInventory(gameObject,StateNameController.playerInventory.slots,
                StateNameController.playerInventory.currentStack);
            for (int i = 0; i < TotalSlots; i++)
            {
                if (_inventory.slots[i].GetItemButton() != null)
                {
                    Instantiate(_inventory.slots[i].GetItemButton(), _playerInventory.slots[i].transform, false);
                    _playerInventory.slots[i].GetComponentInChildren<TextMeshProUGUI>().text = _inventory.currentStack[i].ToString();
                    _playerInventory.currentStack[i] = StateNameController.playerInventory.currentStack[i];
                }
            }
        }
        else
        {
            _health = PlayerMaxHealth;
            _stamina = PlayerMaxStamina;
            //_coinCounterTextScript=GameObject.FindGameObjectWithTag("CoinsAmount").GetComponent<CoinCounterTextScript>();
            //_coinCounterTextScript.setCoinsAmount(_coins);
        }
        healthBarScript.SetMaxHealth(PlayerMaxHealth);
        staminaBarScript.SetMaxStamina(PlayerMaxStamina);
        timeToInvulnerability = invulnerabilityTime;
    }

    public void Update() {
        setStamina(getStamina() + staminaRegenerationSpeed * Time.deltaTime);
        setHealth(getHealth()+healthRegenerationSpeed* Time.deltaTime);
        timeToInvulnerability += Time.deltaTime;
    }

    public void TakeDamage(float damage, bool giveInvulnerability = true)
    {
        if (!_invulnerable) {
            hitEffect.Flash();
            if (timeToInvulnerability > invulnerabilityTime) { //if that time is passed, can take damage
                                                               //_animator.SetTrigger("Hit");
                if (_health > 0) {
                    if (_health - damage < 0) setHealth(0);
                    setHealth(_health - damage);
                }
                if (_health <= 0) {
                    GameOver();
                    Destroy(gameObject);
                    // _animator.SetTrigger("Die");
                }
                if (giveInvulnerability) {
                    timeToInvulnerability = 0;
                }
            }
        }
    }
    public void SetInvulnerability(int value) {
        if(value != 0) {
            _invulnerable = true;
            return;
        }
        _invulnerable = false;
    }

    public bool GetInvulnerability() {
        return _invulnerable;
    }

    public void UseStamina(float stamina) {
        if (_stamina > 0) {
            if (_stamina - stamina < 0) setStamina(0);
            setStamina(_stamina - stamina);
        }
        if (_stamina <= 0) {

        }
    }

    public bool IsAlive()
    {
        return _health>0;
    }

    public void GameOver()
    {
        PlayerInterface.PlayerDeath();
    }

    public void setHealth(float health)
    {
        if (health >= PlayerMaxHealth)
        {
            health = PlayerMaxHealth;
        }

        _health = health;
        healthBarScript.SetHealth(health);
    }
    
    public void setStamina(float stamina)
    {
        if (stamina >= PlayerMaxStamina)
        {
            stamina = PlayerMaxStamina;
        }

        _stamina = stamina;
        staminaBarScript.SetStamina(stamina);
    }

    public float getHealth()
    {
        return _health;
    }
    
    public float getStamina()
    {
        return _stamina;
    }

    public float GetMaxHealth()
    {
        return PlayerMaxHealth;
    }
    
    public float GetMaxStamina()
    {
        return PlayerMaxStamina;
    }

    public void addCoin(int amount) {
        _coins+=amount;
        _coinCounterTextScript.setCoinsAmount(_coins);
    }
}
