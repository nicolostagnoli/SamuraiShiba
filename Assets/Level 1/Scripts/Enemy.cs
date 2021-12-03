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
    private int dropAmount = 5;
    public GameObject blood;

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
            //anim.SetTrigger("die")
        }
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
    public void SetHealth(float health)
    {
        _health = health;
    }

    public float GetHealth() {
        return _health;
    }
    
    public void OnCompleteDieAnimation(){
        DropLoot();
        Destroy(gameObject);
    }
    
}
