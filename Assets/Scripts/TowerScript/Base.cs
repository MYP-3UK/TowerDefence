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
    [SerializeField] private List<GameObject> towers;
    [SerializeField] private GameObject selectedTower;
    [SerializeField] private GameObject targetTower;
    [SerializeField] private int countOfUnitsToExit;

    [SerializeField] private float StartPressTime;
    [SerializeField] private float TimeForDetection = 0.5f;
    [SerializeField] private bool isLongPressing = false;
    [SerializeField] private int mask;

    private LineRenderer lineRenderer;
    private bool isRendered;
    private Vector3 targetPos;
    [SerializeField] Gradient EfficiencyGradient;

    void Start()
    {
        data = Settlement.data;
        UpdateListOfTowers();
        selectedTower = GameObject.FindWithTag("Base");
        ReleaseUnits(data.countofCurrency, selectedTower);
    }
    private void Awake()
    {
        UpdateListOfTowers();
    }

    private void UpdateListOfTowers()
    {
        towers = new List<GameObject>();
        towers = GameObject.FindGameObjectsWithTag("Tower").ToList();
    }

    public void Update()
    {
        UpdateUnitList();
    }

}
