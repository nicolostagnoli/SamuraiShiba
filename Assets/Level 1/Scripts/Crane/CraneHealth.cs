using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneHealth : Enemy
{
    public float maxHealth;

    void Start()
    {
        SetHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetHealth());
    }
}
