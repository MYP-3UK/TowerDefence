using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    List<Unit> units;
    List<Enemy> enemies;

    public void Update()
    {
        if (units == null && enemies == null) { return; }
        foreach (Enemy enemy in enemies)
        {
        }

        foreach (Unit unit in units)
        {
        }
    }
}
