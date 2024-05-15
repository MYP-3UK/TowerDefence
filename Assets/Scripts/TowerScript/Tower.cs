using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] protected List<GameObject> units;
    [SerializeField] protected AnimationCurve Efficiency;

    private void Update()
    {
        UpdateUnitList();
    }
    
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

    public float CalcEfficiency()
    {
        UpdateUnitList();
        return Efficiency.Evaluate(units.Count);
    }

    public int GetUnitsCount()
    {
        UpdateUnitList();
        return units.Count;
    }

    public void UpdateUnitList()
    {
        for (int i = units.Count - 1; i >= 0; i--)
        {
            if (units[i].IsDestroyed() || units[i].GetComponent<MovableObject>().isDead)
            {
                units.RemoveAt(i);
            }
        }
    }
}

