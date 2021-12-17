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
    private int dropAmount = 1;
    public GameObject blood;
    public HitEffect hitEffect;

    public float invulnerabilityTime;
    private float timeToInvulnerability;
    private void Start() {
        timeToInvulnerability = 0; 
    }

    private void Update() {
        timeToInvulnerability += Time.deltaTime;
    }

    public virtual void TakeDamage(float damage)
    {
        if (timeToInvulnerability >= invulnerabilityTime) {
            timeToInvulnerability = 0;

            hitEffect.Flash();
            _health -= damage;
            if (_health <= 0) {
                Instantiate(blood, transform.position, Quaternion.identity);
                DropLoot();
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
                Instantiate(item.gameObject, transform.position+new Vector3(Random.Range(-2f,2f), Random.Range(0, 1)), Quaternion.identity);
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
