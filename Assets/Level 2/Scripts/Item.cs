using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float itemCooldown=1;

    public abstract void useItem();


}
