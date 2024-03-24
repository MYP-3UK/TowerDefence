using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
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
        for (int i = math.clamp(units.Count,0, Count); i >= 0; i--)
        {
            units[i].GetComponent<UnitMovement>().SetTarget(target);
            units[i].SetActive(true);
            units.Remove(units[i]);
        }
    }

}
