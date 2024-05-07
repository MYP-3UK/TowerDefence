using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public enum EntityType
{
    Settler,
    Samurai,
    Haijin,
    Maekusha
}
[System.Serializable]   
public class EntityData
{
    public string id;
    public string name;
    public string type;
    public float moveSpeed;
    public float attackSpeed;
    public int health;
    public int durability;
    public int vulnerability;
    public int distanceToEnter;

    public EntityData(string id, string Name, string type, float MoveSpeed, float AttackSpeed, int Health, int Durability, int Vulnerability, int DistanceToEnter)
    {
        this.id = id;
        name = Name;
        this.type = type;
        moveSpeed = MoveSpeed;
        attackSpeed = AttackSpeed;
        health = Health;
        durability = Durability;
        vulnerability = Vulnerability;
        distanceToEnter = DistanceToEnter;
    }   
}

public class EntityinJson : MonoBehaviour
{
    [SerializeField] public EntityData entityData;
    public string idunit;

    public void Start()
    {
        GetStats(idunit);
    }

    public EntityData GetStats(string idunit)
    {
        string fileName = "EntityList.json";
        string path = Path.Combine(Application.dataPath, "Configs", fileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path).Trim();
            Dictionary<string, EntityData> entityDataDict = JsonConvert.DeserializeObject<Dictionary<string, EntityData>>(json);

            if (entityDataDict != null)
            {
                if (entityDataDict.TryGetValue(idunit, out EntityData data))
                {
                    Debug.Log("data::" + data.distanceToEnter);
                    entityData = data;
                    return entityData;
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
    //public void SaveStats()
    //{
    //    File.WriteAllText(Path.Combine("Configs", "EntityList.json"), JsonUtility.ToJson(_json));
    //}    
}




