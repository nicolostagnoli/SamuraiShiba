using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerStats : MonoBehaviour
{
    public float PlayerMaxStamina;
    private float PlayerMaxHealth;
    private int _coins=1;
    private float _health;
    private float _stamina;
    public HealthBarScript healthBarScript;
    public StaminaBarScript staminaBarScript;
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

    [Header("SoundEffects")] 
    public GameObject woodenKatanaSounds;

    private void Start()
    {
        PlayerMaxHealth = StateNameController.playerMaxHealth;
        healthRegenerationSpeed = StateNameController.healthRegeneration;
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _health = PlayerMaxHealth;
        _stamina = PlayerMaxStamina;
        if (StateNameController.playerHealth != 0 && StateNameController.playerStamina != 0)
        {
            _inventory = StateNameController.playerInventory;
            _playerInventory.setSlots(_inventory.slots);

            for (int i = 0; i < TotalSlots; i++)
            {
                if (_inventory.slots[i].GetItemButton() != null)
                {
                    if (_inventory.currentStack[i] > 0)
                    {
                        Instantiate(_inventory.slots[i].GetItemButton(), _playerInventory.slots[i].transform, false);
                        _playerInventory.slots[i].GetComponentInChildren<TextMeshProUGUI>().text = _inventory.currentStack[i].ToString();
                        _playerInventory.currentStack[i] = StateNameController.playerInventory.currentStack[i];
                    }

                }
            }
        }

        if (StateNameController.HealthPotionQuantity != 0 || StateNameController.StaminaPotionQuantity != 0 ||
            StateNameController.ShurikenQuantity != 0)
        {
            StateNameController.playerInventory = _playerInventory;
            _inventory = StateNameController.playerInventory;
            _inventory.currentStack[0] += StateNameController.HealthPotionQuantity;
            StateNameController.HealthPotionQuantity = 0;
            _inventory.currentStack[1] += StateNameController.StaminaPotionQuantity;
            StateNameController.StaminaPotionQuantity = 0;
            _inventory.currentStack[2] += StateNameController.ShurikenQuantity;
            StateNameController.ShurikenQuantity = 0;
            _inventory.slots[0].SetItemButton(StateNameController.HealthPotion);
            _inventory.slots[1].SetItemButton(StateNameController.StaminaPotion);
            _inventory.slots[2].SetItemButton(StateNameController.Shuriken);
            
            for (int i = 0; i < TotalSlots; i++)
            {
                Debug.Log(_inventory.slots[i].GetItemButton() != null);
                if (_inventory.slots[i].GetItemButton() != null)
                {
                    if (_inventory.currentStack[i] > 0)
                    {
                        Instantiate(_inventory.slots[i].GetItemButton(), _playerInventory.slots[i].transform, false);
                        _playerInventory.slots[i].GetComponentInChildren<TextMeshProUGUI>().text = _inventory.currentStack[i].ToString();
                        _playerInventory.currentStack[i] = StateNameController.playerInventory.currentStack[i];
                    }

                }
            }
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

    public void PlayWoodenKatanaSound()
    {
        int randomValue = Random.Range(0, 4);
        woodenKatanaSounds.GetComponents<AudioSource>()[randomValue].Play();
    }
    
}
