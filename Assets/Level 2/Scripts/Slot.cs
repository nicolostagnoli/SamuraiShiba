using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Inventory _inventory;
    public int slotNumber;
    private Item _item;
    public KeyCode key;
    private Button button;
    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        button = GetComponent<Button>();
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
            _item = GetComponentInChildren<Item>();
            _item.useItem();
            _inventory.currentStack[slotNumber]--;
            GetComponentInChildren<TextMeshProUGUI>().text = _inventory.currentStack[slotNumber].ToString();
            if (_inventory.currentStack[slotNumber] == 0)
            {
                for (int i = 1; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
        }
    }
}
