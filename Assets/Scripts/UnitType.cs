using UnityEngine;
using System.Collections.Generic;
using System.IO;


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
    public static EntityList CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<EntityList>(jsonString);
        }
    void Start()
    {
        
        string fileName = "Entity.json";
        string path = Path.Combine(Application.dataPath, "Scripts", fileName);

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            EntityList entityList = JsonUtility.FromJson<EntityList>(jsonString); // CreateFromJSON(jsonString);  // 
            if (entityList != null && entityList.Entity != null)
            {
                foreach (var entity in entityList.Entity)
                {
                    Debug.Log("Entity ID: " + entity.Key);
                    EntityData data = entity.Value;
                    if (data != null)
                    {
                        Debug.Log("Name: " + data.Name);
                        Debug.Log("Type: " + data.type);
                        Debug.Log("Move Speed: " + data.MoveSpeed);
                        Debug.Log("Attack Speed: " + data.AttackSpeed);
                        Debug.Log("Health: " + data.Health);
                        Debug.Log("Durability: " + data.Durability);
                        Debug.Log("Vulnerability: " + data.Vulnerability);
                    }
                    else
                    {
                        Debug.LogError("EntityData is null for ID: " + entity.Key);
                    }
                }
            }
            else
            {
                Debug.LogError("EntityList or EntityList.Entity is null.");
            }
        }
        else
        {
            Debug.LogError("File " + fileName + " not found.");
        }
    }
}