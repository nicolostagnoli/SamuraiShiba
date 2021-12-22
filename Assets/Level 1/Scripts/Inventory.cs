using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
   public Slot[] slots;
   public int[] currentStack;
   public static Inventory CreateInventory(GameObject where, Slot[] _slots, int[] _currentStack)
   {
      Inventory myInventory = where.AddComponent<Inventory>();
      myInventory.slots = _slots;
      myInventory.currentStack = _currentStack;
      return myInventory;
   }

   public void setSlots(Slot[] _slots)
   {
      for (int i = 0; i < _slots.Length; i++)
      {
         slots[i].SetItemButton(_slots[i].GetItemButton());
      }
   }

}
