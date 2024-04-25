using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Unity.Mathematics;
using UnityEngine;
using System.Linq;

public class WarriorTower : Tower
{
    [SerializeField] GameObject warrior;
    [SerializeField] Dictionary<GameObject, GameObject> warriors;
    [SerializeField] public float PatrolDistance;

    public void Awake()
    {
        warriors = new Dictionary<GameObject, GameObject>();
    }

    public override void EnterUnit(GameObject unit)
    {
        Debug.Log("Зашёл");
        units.Add(unit);
        var _warrior = Instantiate(warrior, unit.transform.position, Quaternion.identity, transform);
        _warrior.transform.position = unit.transform.position;
        _warrior.GetComponent<Warrior>().MotherTower = gameObject;
        warriors.Add(unit, _warrior);
        unit.SetActive(false);
    }
    public override void ReleaseUnits(int Count, GameObject target)
    {
        int unitCount = math.min(Count, units.Count);
        for (int i = unitCount - 1; i >= 0; i--)
        {
            units[i].SetActive(true);
            units[i].transform.position = warriors[units[i]].transform.position;
            units[i].GetComponent<Unit>().SetTarget(target);

            var _warrior = warriors[units[i]];
            warriors.Remove(units[i]);
            Destroy(_warrior);

            units.RemoveAt(i);
        }
    }

    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, Vector3.forward, PatrolDistance);
    }
}
