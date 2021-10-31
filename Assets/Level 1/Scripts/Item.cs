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

    public void setCategory(Category category)
    {
        this.category = category;
    }

    public Category getCategory()
    {
        return category;
    }
    
    public void setItemName(ItemName itemName)
    {
        _itemName = itemName;
    }

    public ItemName getItemName()
    {
        return _itemName;
    }

}
