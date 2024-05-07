using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawElements : MonoBehaviour
{
    public GameData gameData;
    public SavedData data;
    public TMP_Text moneycounter;
    [SerializeField] public TMP_Text text;
    [SerializeField] public double allmoney;
    public void Draw(SavedData data)
    {
        GameObject moneyCounterObject = GameObject.Find("moneycounter");
        moneycounter = moneyCounterObject.GetComponent<TMP_Text>();
        moneycounter.text = allmoney.ToString("#");
    }

    //public SavedData DrawCapturedSettlements(int data)
    //{
    //    gameData = new GameData();
    //    savedData = gameData.Start();
    //    Debug.Log("CapturedSettlements: " + savedData.CapturedSettlements);
    //    OnDrawCapturedSettlements(data);
    //    return savedData;
    //}
    //public TMP_Text uncapturedsettlements;
    //public SavedData DrawunCapturedSettlements(int data)
    //{
    //    gameData = new GameData();
    //    savedData = gameData.Start();
    //    Debug.Log("CapturedSettlements: " + savedData.CapturedSettlements);
    //    OnDrawCapturedSettlements(data);
    //    return savedData;
    //}
    //public void OnDrawCapturedSettlements(double count)
    //{
    //    GameObject settlementsCounterObject = GameObject.Find("capturedsettlements");
    //    capturedsettlements = settlementsCounterObject.GetComponent<TMP_Text>();
    //    capturedsettlements.text = count.ToString("#");
    //}
    //public void OnDrawunCapturedSettlements(double count)
    //{
    //    GameObject settlementsCounterObject = GameObject.Find("uncapturedsettlements");
    //    capturedsettlements = settlementsCounterObject.GetComponent<TMP_Text>();
    //    capturedsettlements.text = count.ToString("#");
    //}
}
