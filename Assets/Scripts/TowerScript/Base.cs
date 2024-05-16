using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Base : Tower
{
    public Settlement Settlement;
    public Settlements data;
    [SerializeField] GameObject unitprefab;
    [SerializeField] private int unitcounter;
    void Start()
    {
        data = Settlement.data;
        Debug.Log("unit i ndata " + data.countofUnits);
        for(int i =0; i < unitcounter; i++)
        {
            var unit = GameObject.Instantiate(unitprefab, transform);
            unit.GetComponent<MovableObject>().SetTarget(gameObject);
            //units.Add(unit);
            unit = null;
        }

    }
    private void Awake()
    {

    }
    

}
