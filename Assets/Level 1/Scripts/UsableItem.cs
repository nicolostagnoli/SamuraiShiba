using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UsableItem : Item
{
    private float itemCooldown=1;
    public abstract void useItem();
    
    public void initCategory()
    {
        setCategory(Category.UsableItem);
    }


}
