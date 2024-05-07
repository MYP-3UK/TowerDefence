using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Settlement : MonoBehaviour
{
    private SettlementfromJSON settlementfromJSON = new();
    private SettlementsData settlementData;
    //public TMP_Text uncapturesssdsettlements;
    [SerializeField] public string idsettlement = "";
    public void Start()
    {
        settlementfromJSON.GetStats(idsettlement);
        CaptureSettlement();
    }

    void Update()
    {
        

    }

    public SettlementsData CaptureSettlement()
    {

        return null;
    }


}
