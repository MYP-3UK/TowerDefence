using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class MoneyHandler : MonoBehaviour
{
    public UnityEvent<DrawElements> OnDrawMoney;
    public double allmoney;
    private DrawElements draw; // Этот объект не используется, уберите его, если он не нужен
    public GameData gameData;
    public SavedData data;
    public TMP_Text moneycounter;
    public TimeTracker tracker;
 

    void Start()
    {
        data = gameData.data;
        allmoney = data.OutsideCurrency;
        Debug.Log("Data in money" + data.OutsideCurrency);
        gameData.DrawSettlements();
        StartCoroutine(MoneyCycle());

    }
    IEnumerator MoneyCycle()
    {
        while (allmoney <= 1000)
        {
            allmoney += 1;
            moneycounter.text = allmoney.ToString("#");
            //Debug.Log(allmoney);
            yield return new WaitForSeconds(0.5f);
        }


    }

    public void Wallet(double money)
    {
        allmoney = money;
    }
    public double PassedMoney(double secondsPassed, double data)
    {
        draw = new DrawElements();
        double resourcePerSecond = 0.0015d;
        data += resourcePerSecond * secondsPassed;
        allmoney = data;
        Debug.Log("Allmoney in passed:" + allmoney);
        //OnDrawMoney.AddListener(Wallet);
        return data;
    }
}
