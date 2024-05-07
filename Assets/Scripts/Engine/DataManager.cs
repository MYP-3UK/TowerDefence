using System.Collections;
using static System.Collections.IEnumerable;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    private GameData gameData;

    [SerializeField] private string fileName;

    public static DataManager instance { get; private set; }

    private DataHandler dataHandler;
    private void Awake()
    {
        if (instance == null)
        {
            Debug.LogError("Not Found");
        }
        instance = this;
    }

    private List<IDataManager> dataPersistenceObjects;

    public void NewGame()
    {
       this.gameData = new GameData();
    }

    private void Start()
    {
        this.dataHandler = new DataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.LogError("Not data was found. Creating default");
            NewGame();
        }

        foreach (IDataManager dataPersistenceObjects in dataPersistenceObjects)
        {
            dataPersistenceObjects.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        foreach (IDataManager dataPersistenceObjects in dataPersistenceObjects)
        {
            dataPersistenceObjects.SaveData(ref gameData);
        }
        
        dataHandler.Save(gameData);
    }

    private void Exit()
    {
        SaveGame();
    }

    private List<IDataManager> FindAllDataPersistanceObjects()
    {
        IDataManager[] dataPersistenceObjectsArray = FindObjectsOfType<MonoBehaviour>().OfType<IDataManager>().ToArray();
        return new List<IDataManager>(dataPersistenceObjects);
    }
}

