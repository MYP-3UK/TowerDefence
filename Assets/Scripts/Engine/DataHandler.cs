using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class DataHandler : MonoBehaviour
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public DataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadeddata = null;
        if(File.Exists(fullPath))
        {
            string datatoload = "";
            using(FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using(StreamReader reader = new StreamReader(stream))
                {
                    datatoload = reader.ReadToEnd();
                }
            }
            loadeddata = JsonUtility.FromJson<GameData>(datatoload);
        }
        else
        {
            Debug.Log("Error in opening config file");
        }
        return loadeddata;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if (File.Exists(fullPath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string datatostore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(datatostore);
                }
            }
        }else
        {
            Debug.Log("Error to open config");
        }
    }


}
