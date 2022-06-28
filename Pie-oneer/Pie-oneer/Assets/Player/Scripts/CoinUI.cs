using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public Inventory inventory;

    private TextMeshProUGUI coinText;

    void Start()
    {
        inventory.CoinsUpdated += CoinUpdateCount;
        coinText = gameObject.transform.Find("coinTxt").GetComponentInChildren<TextMeshProUGUI>();
    }

    private void CoinUpdateCount(object sender, CoinEventArgs args)
    {
        coinText.text = $"{args.CoinBalance}";
    }
}
