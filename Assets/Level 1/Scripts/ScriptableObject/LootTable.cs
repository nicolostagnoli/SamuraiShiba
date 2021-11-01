using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Loot
{
    public Item item;
    public int lootChanche;
}
[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;
    private List<Item> droppedItems=new List<Item>();

    public List<Item> itemPowerup(int dropAmount)
    {
        for (int k = 0; k < dropAmount; k++)
        {
            int cumProb=0;
            int currentProb = Random.Range(0, 100);
            for (int i = 0; i < loots.Length; i++)
            {
                cumProb += loots[i].lootChanche;
                if (currentProb <= cumProb)
                {
                    droppedItems.Add(loots[i].item);
                    break;
                }
            }
        }
        return droppedItems;
    }
}