using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] List<GameObject> units;

    public void EnterUnit(GameObject unit)
    {
        units.Add(unit);
        unit.SetActive(false);
    }

    public void ReleaseUnits(int Count, GameObject target)
    {
        for (int i = math.clamp(Count - 1,0,units.Count); i >= 0; i--)
        {
            units[i].GetComponent<Unit>().SetTarget(target);
            units[i].SetActive(true);
            units.Remove(units[i]);
        }
    }
}

