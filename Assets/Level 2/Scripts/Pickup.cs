using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory _inventory;
    public GameObject itemButton;
    [SerializeField]
    private int _maxStack=1;
    //If we modify the slot component by adding more children to it we've to modify the ItemPosition
    private const int ItemPosition=1;

    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isFound=false;
        int firstFreeSlot=0;
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < _inventory.slots.Length; i++)
            {
                //Slot[i] is empty and can be used for the next item
                if (_inventory.currentStack[i] ==0 )
                {
                    firstFreeSlot = i;
                    break;
                }
                //Item already present in the inventory
                if (_inventory.slots[i].transform.GetChild(ItemPosition).gameObject.tag.Equals(gameObject.tag) && _inventory.currentStack[i] < _maxStack)
                {
                    _inventory.currentStack[i]++;
                    Destroy(gameObject);
                    _inventory.slots[i].GetComponentInChildren<TextMeshProUGUI>().text = _inventory.currentStack[i].ToString();
                    isFound = true;
                    break;
                }
                
            }
            //Item not found in the inventory
            if (!isFound)
            {
                _inventory.currentStack[firstFreeSlot]++;
                Instantiate(itemButton, _inventory.slots[firstFreeSlot].transform, false);
                _inventory.slots[firstFreeSlot].GetComponentInChildren<TextMeshProUGUI>().text = _inventory.currentStack[firstFreeSlot].ToString();
                Destroy(gameObject);
            }
        }
    }
}
