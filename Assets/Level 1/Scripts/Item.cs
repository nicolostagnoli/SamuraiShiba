using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Category
{
    UsableItem,
    Weapons
}
public enum ItemName
{
    HealthPotion,
    StaminaPotion,
    Coin
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

}
