using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : UsableItem
{
    private ShurikenAttack _shurikenAttack;
    private void Start()
    {
        _shurikenAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<ShurikenAttack>();
        InitCategory();
        SetItemName(ItemName.Shuriken);
    }

    public override void UseItem()
    {
        _shurikenAttack.Attack();
    }
    
    public override bool CanUseItem()
    {
        return _shurikenAttack.canAttack();
    }

}
