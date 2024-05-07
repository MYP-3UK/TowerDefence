using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SettlementsData
{
    public string Id;
    public bool captured;
    public string type;
    public int countofUnits;
    public double countofCurrency;
    public int countofTownholders;
    public int countofBuildings;
    public string townholders;
    public SettlementsData(string id, bool Captured, string type, int CountofUnits, double CountofCurrency, int CountofTownholders, int CountofBuildings, string Townholders)
    {
        this.Id = id;
        captured = Captured;
        this.type = type;
        countofUnits = CountofUnits;
        countofCurrency = CountofCurrency;
        countofTownholders = CountofTownholders;
        countofBuildings = CountofBuildings;
        townholders = Townholders;
    }
}


public class SettlementfromJSON : MonoBehaviour
{
    //new private string idsettlement;
    public string idsettlement;
    private SettlementsData settlementsData;

    public void Start()
    {
        GetStats(idsettlement);

    }
    public SettlementsData GetStats(string idsettlement)
    {

        string fileName = "Settlements.json";
        string path = Path.Combine(Application.dataPath, "Configs", fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path).Trim();
            Dictionary<string, SettlementsData> settlementsDataDict = JsonConvert.DeserializeObject<Dictionary<string, SettlementsData>>(json);

            if (settlementsDataDict != null)
            {
                if (settlementsDataDict.TryGetValue(idsettlement, out SettlementsData data))
                {
                    settlementsData = data;
                    return settlementsData;
                }
                else
                {
                    //Debug.LogError("Entity with ID " + idunit + " not found.");
                    return null;
                }
            }
            else
            {
                Debug.LogError("Failed to deserialize JSON to Dictionary<string, EntityData>.");
                return null;
            }

        }
        else
        {
            Debug.Log("File " + fileName + " not found.");
            return null;
        }

    }

}
