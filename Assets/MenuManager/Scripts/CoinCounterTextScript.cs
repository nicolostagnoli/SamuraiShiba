using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounterTextScript : MonoBehaviour
{
    private TextMeshProUGUI _text;
    public int coinAmount;
    
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.SetText(coinAmount.ToString());
        Debug.Log(coinAmount.ToString());
    }
}
