using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UsableItem : Item
{
    private float itemCooldown=1;
    public abstract void UseItem();
    
    public void InitCategory()
    {
        SetCategory(Category.UsableItem);
    }

    public abstract bool CanUseItem();


}
