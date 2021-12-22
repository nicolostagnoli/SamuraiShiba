using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

public enum Category
{
    UsableItem,
    Weapons
}
public enum ItemName
{
    HealthPotion,
    StaminaPotion,
    Shuriken,
    Coin,
    NotFound=100
}
public abstract class Item : MonoBehaviour
{
    private Category category;
    private ItemName _itemName;

    public void SetCategory(Category category)
    {
        this.category = category;
    }

    public Category GetCategory()
    {
        return category;
    }
    
    public void SetItemName(ItemName itemName)
    {
        _itemName = itemName;
    }

    public ItemName GetItemName()
    {
        return _itemName;
    }

    public static ItemName getItemBySlot(int slotNumber)
    {

        switch (slotNumber)
        {
            case  0:
                return ItemName.HealthPotion;
            case  1:
                return ItemName.StaminaPotion;
            case  2:
                return ItemName.Shuriken;
        }

        return ItemName.NotFound;
    }

    public static int getSlotNumberByItem(ItemName itemName)
    {
        switch (itemName)
        {
            case ItemName.HealthPotion:
                return 0;
            case ItemName.StaminaPotion:
                return 1;
            case ItemName.Shuriken:
                return 2;
        }

        return -1;
    }

}
