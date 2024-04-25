using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] protected List<GameObject> units;

    public virtual void EnterUnit(GameObject unit)
    {
        units.Add(unit);
        unit.SetActive(false);
    }
    public virtual void ReleaseUnits(int Count, GameObject target)
    {
        int unitCount = math.min(Count, units.Count);
        for (int i = unitCount - 1; i >= 0; i--)
        {
            units[i].SetActive(true);
            units[i].GetComponent<Unit>().SetTarget(target);
            units.RemoveAt(i);
        }
    }
}

