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
    private ItemName _itemName;
    private int _slotNumber;

    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _itemName = GetComponent<UsableItem>().GetItemName();
        _slotNumber = Item.getSlotNumberByItem(_itemName);
        Debug.Log(_itemName);
        if (other.CompareTag("Player"))
        {
            //Item not found in the inventory
            if (_inventory.currentStack[_slotNumber]==0 )
            {
                _inventory.currentStack[_slotNumber]++;
                Instantiate(itemButton, _inventory.slots[_slotNumber].transform, false);
                _inventory.slots[_slotNumber].GetComponentInChildren<TextMeshProUGUI>().text = _inventory.currentStack[_slotNumber].ToString();
                _inventory.slots[_slotNumber].SetItemButton(itemButton);
                Destroy(gameObject);
            }
            else
            {
                _inventory.currentStack[_slotNumber]++;
                Destroy(gameObject);
                _inventory.slots[_slotNumber].GetComponentInChildren<TextMeshProUGUI>().text = _inventory.currentStack[_slotNumber].ToString();
            }
        }
    }
}
