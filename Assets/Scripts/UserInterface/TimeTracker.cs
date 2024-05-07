using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static TimeTracker;

public class TimeTracker : MonoBehaviour
{
    [System.Serializable]
    public class LoadingData
    {
        public DateTime lastLogin;
        public DateTime lastLogout;
    }



    private SavedData gameData;

    public void Awake()
    {
        //LoadLoginData(gameData.timeofentrance);
        UpdateLogoutTime();
        //SaveLoginData(gameData.TimeofExit);
    }

    public GameData LoadLoginData(string json)
    {
        Debug.Log("I'm Loading");
        return null;
    }
    
    private LoadingData loadingData;
    public DateTime UpdateLogoutTime()
    {
        LoadingData loadingData = new LoadingData();
        loadingData.lastLogout = DateTime.Now;
        return loadingData.lastLogout;
    }



    LoadingData SaveLoginData(DateTime timeofexit)
    {
        return loadingData;
    }
}
