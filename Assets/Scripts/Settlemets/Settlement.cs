using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Settlement : MonoBehaviour
{
    public TMP_Text currency;
    public SettlementfromJSON settlementfromJSON = new();
    [SerializeField] public string idsettlement;
    [SerializeField] public Settlements data;

    public void Start()
    {
        settlementfromJSON = new();
        data = settlementfromJSON.GetStats(idsettlement);
    }
    public void Update()
    {
        currency.text = data.countofCurrency.ToString("#");
    }
}
