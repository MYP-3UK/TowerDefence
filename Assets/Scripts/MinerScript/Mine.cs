using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Mine : Tower
{
    [SerializeField] public float resourcespersecond;
    [SerializeField] public float resources;
    public Settlement settlement;
    public Settlements data;
    float efficiency;
    private void Start()
    {
        data = settlement.data;
        Debug.Log("resources data " + data.countofCurrency);
    }
    public void Update()
    {
        efficiency = CalcEfficiency();
        resources += resourcespersecond * efficiency*Time.deltaTime;
        data.countofCurrency = resources;
        
        //Debug.Log("resources data all time  " + data.countofCurrency);
    }

}
