using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory _inventory;
    public GameObject itemButton;
    [SerializeField]
    private int _maxStack=1;

    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isFound=false;
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < _inventory.slots.Length; i++)
            {
                if (_inventory.isFull[i] == false) _inventory.firstFreeSlot = i;
                if (_inventory.slots[i].tag.Equals(gameObject.tag) && _inventory.currentStack[i] < _maxStack)
                {
                    _inventory.currentStack[i]++;
                    if(_inventory.currentStack[i]== _maxStack) _inventory.isFull[i] = true;
                    Destroy(gameObject);
                    Debug.Log("current stack at position i " + _inventory.currentStack[i]);
                    _inventory.counterText[i].text = _inventory.currentStack[i].ToString();
                    isFound = true;
                    break;
                }
                
            }
            if (!isFound)
            {
                _inventory.slots[_inventory.firstFreeSlot].tag = gameObject.tag;
                _inventory.currentStack[_inventory.firstFreeSlot]++;
                _inventory.counterText[_inventory.firstFreeSlot].text = _inventory.currentStack[_inventory.firstFreeSlot].ToString();
                Instantiate(itemButton, _inventory.slots[_inventory.firstFreeSlot].transform, false);
                Destroy(gameObject);
            }
        }
    }
}
