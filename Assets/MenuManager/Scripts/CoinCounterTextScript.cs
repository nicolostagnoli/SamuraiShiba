using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounterTextScript : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();

    }
    public void setCoinsAmount(int coins)
    {
        _text.SetText(coins.ToString());
    }
    

} 
