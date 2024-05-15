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
    public Settlements data;
    //private SettlementsData settlementData;
    //public TMP_Text uncapturesssdsettlements;
    public void Start()
    {
        data = settlementfromJSON.GetStats(idsettlement);
    }
    public void Update()
    {
        currency.text = data.countofCurrency.ToString("#");
    }
}
