using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

[System.Serializable]
public class EntityData
{
    public string Name;
    public string type;
    public float MoveSpeed;
    public float AttackSpeed;
    public int Health;
    public int Durability;
    public int Vulnerability;
}

[System.Serializable]
public class EntityList
{
    public Dictionary<string, EntityData> Entity;
    public Dictionary<string, Dictionary<string, string>> TownHolders;
}


public class UnitType : MonoBehaviour
{
    public void Start()
    {
        string fileName = "config.json";
        string path = Path.Combine(Application.dataPath, "Scripts", fileName);
        Debug.Log("Path: " + path);

        if (File.Exists(path))
        {
            // Read the json file where primary key is entity
            string json = File.ReadAllText(path).Trim();
            // Convert the json to object
            EntityList entityList = JsonConvert.DeserializeObject<EntityList>(json) ??
                                    throw new InvalidOperationException(
                                        "Failed to deserialize json to EntityList object.");
            foreach (var entity in entityList.Entity)
            {
                Debug.Log("Entity ID: " + entity.Key);
                EntityData data = entity.Value;
                Debug.Log("Name: " + data.Name);
                Debug.Log("Type: " + data.type);
                Debug.Log("Move Speed: " + data.MoveSpeed);
                Debug.Log("Attack Speed: " + data.AttackSpeed);
                Debug.Log("Health: " + data.Health);
                Debug.Log("Durability: " + data.Durability);
                Debug.Log("Vulnerability: " + data.Vulnerability);
            }
        }
        else
        {
            Debug.Log("File " + fileName + " not found.");
        }
    }
}