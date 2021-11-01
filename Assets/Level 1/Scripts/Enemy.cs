using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private float _health=50;
    public LootTable lootTable;
    private int dropAmount = 5;
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger("Hit");
        Debug.Log(_health);
        _health -= damage;
        if (_health <= 0)
        {
            _animator.SetTrigger("Die");
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
    
    public void OnCompleteDieAnimation(){
        DropLoot();
        Destroy(gameObject);

    }
    
}
