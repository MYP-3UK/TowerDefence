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
    //public UnityEvent<DrawElements> OnDrawMoney;
    private DrawElements draw; // Этот объект не используется, уберите его, если он не нужен
    [SerializeField] public GameData gameData;
    [SerializeField] public SavedData Data;
    public TMP_Text moneycounter;
    public TimeTracker tracker;

    [SerializeField] public double allmoney;

    void Start()
    {
        Data = gameData.data;
        allmoney = Data.OutsideCurrency;
        Debug.Log("Data in money "  + allmoney);
        StartCoroutine(MoneyCycle());
    }
    IEnumerator MoneyCycle()
    {
        while (allmoney <= 2000)
        {
            allmoney += 1;
            moneycounter.text = allmoney.ToString("#");
            //Debug.Log(allmoney);
            yield return new WaitForSeconds(0.7f);
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
