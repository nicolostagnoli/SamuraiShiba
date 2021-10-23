using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
   public bool[] isFull;
   public GameObject[] slots;
   public int[] currentStack;
   public int firstFreeSlot;
   public TextMeshProUGUI[] counterText;
}
