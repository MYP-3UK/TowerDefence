using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

[System.Serializable]
public class Settlements
{
    public bool captured;
    public int countofUnits;
    public int countofCurrency;
    public int countofTownholders;
    public int countofBuildings;
    public string townholders;
    public Settlements (bool Captured, int CountofUnits, int CountofCurrency, int CountofTownholders, int CountofBuildings, string Townholders) 
    {
        captured = Captured;
        countofUnits = CountofUnits;
        countofCurrency = CountofCurrency; 
        countofTownholders = CountofTownholders;
        countofBuildings = CountofBuildings;
        townholders = Townholders;
    }
}


public class SettlementfromJSON : MonoBehaviour
{
    public string idsettlement;
    public Settlements settlementsData;
    public double currency;
    public Dictionary<string, Settlements> settlementsDataDict;

    //void Start()
    //{
    //} 
    public Settlements GetStats(string idsettlement)
    {
        string fileName = "Settlements.json";
        string path = Path.Combine(Application.dataPath, "Configs", fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path).Trim();
            settlementsDataDict = JsonConvert.DeserializeObject<Dictionary<string, Settlements>>(json);
        }
        if (settlementsDataDict.TryGetValue(idsettlement, out Settlements data))
        {
            settlementsData = data;
            return data;
        }
        return data;
    }
}