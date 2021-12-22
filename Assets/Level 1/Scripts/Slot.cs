using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Inventory _inventory;
    //Assign whatever number you want, but must be different for every slot
    public int slotNumber;
    private UsableItem _item;
    public KeyCode key;
    private Button button;
    private GameObject _itemButton;
    private ItemName _slotItem;
    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        button = GetComponent<Button>();
        _slotItem = Item.getItemBySlot(slotNumber);
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            FadeToColor(button.colors.pressedColor);
            button.onClick.Invoke();
        }
        else if (Input.GetKeyUp(key))
        {
            FadeToColor(button.colors.normalColor);
        }
    }

    void FadeToColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, button.colors.fadeDuration, true, true);
    }

    public void UseItem()
    {
        if (_inventory.currentStack[slotNumber] > 0)
        {
            _item = GetComponentInChildren<UsableItem>();
            if(_item.CanUseItem())
            {
                _item.UseItem();
                _inventory.currentStack[slotNumber]--;
                updateItemQuantityText();
                if (_inventory.currentStack[slotNumber] == 0)
                {
                    for (int i = 2; i < transform.childCount; i++)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                    }
                }
            }

        }
    }

    private void updateItemQuantityText()
    {
        if (_inventory.currentStack[slotNumber] == 0) GetComponentInChildren<TextMeshProUGUI>().text = "";
        else GetComponentInChildren<TextMeshProUGUI>().text = _inventory.currentStack[slotNumber].ToString();
    }

    public GameObject GetItemButton()
    {
        return _itemButton;
    }

    public void SetItemButton(GameObject item)
    {
        _itemButton = item;
    }
}
