using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _health=50;
    public LootTable lootTable;
    private int dropAmount = 10;
    public void TakeDamage(float damage)
    {
        Debug.Log(_health);
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
            DropLoot();
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
    
    
}
