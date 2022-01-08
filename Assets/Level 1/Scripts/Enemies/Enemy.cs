using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class Enemy : MonoBehaviour
{
    private float _health=50;
    public LootTable lootTable;
    [SerializeField]
    private int dropAmount = 5;
    public GameObject blood;
    public HitEffect hitEffect;
    public GameObject woodenHitEffect;
    public HealthBarScript _healthBarScript;


    public float invulnerabilityTime;
    private float timeToInvulnerability;
    private void Start() {
        timeToInvulnerability = invulnerabilityTime; 
    }

    public void Update() {
        timeToInvulnerability += Time.deltaTime;
    }

    public virtual void TakeDamage(float damage)
    {
        if (timeToInvulnerability >= invulnerabilityTime) {
            timeToInvulnerability = 0;

            hitEffect.Flash();
            _health -= damage;
            Instantiate(woodenHitEffect, transform.position, Quaternion.identity, transform);
            if (_healthBarScript!= null)_healthBarScript.SetHealth(GetHealth());
            if (_health <= 0) {
                Instantiate(blood, transform.position, Quaternion.identity);
                DropLoot();
                if (gameObject.name == "Crane")
                {
                    StateNameController.jumpForce = 10f;
                }

                if (gameObject.name == "Wolf")
                {
                    StateNameController.dashSpeed = 5f;
                }
                Destroy(gameObject);
                //anim.SetTrigger("die")
            }
        }
    }

    protected bool isVulnerable() {
        return timeToInvulnerability >= invulnerabilityTime;
    }
    
    public void DropLoot()
    {
        if (lootTable != null)
        {
            List<Item> current = lootTable.itemPowerup(dropAmount);
            foreach (Item item in current)
            {
                Instantiate(item.gameObject, transform.position+new Vector3(Random.Range(-0.2f,0.2f), Random.Range(0f, 0f)), Quaternion.identity);
            }
        }
    }
    public virtual void SetHealth(float health)
    {
        _health = health;
    }

    public float GetHealth() {
        return _health;
    }
    
    public virtual void OnCompleteDieAnimation(){
        DropLoot();
        Destroy(gameObject);
    }
    
}
