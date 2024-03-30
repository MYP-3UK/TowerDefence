using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private List<GameObject> towers = new List<GameObject>();
    [SerializeField] private GameObject selectedTower;
    [SerializeField] private int countOfUnitsToExit;
    [SerializeField] TouchHandler touchHandler;

    private void Awake()
    {
        touchHandler = GetComponent<TouchHandler>() ?? gameObject.AddComponent<TouchHandler>();
        UpdateListOfTowers();
    }

    private void UpdateListOfTowers()
    {
        towers = GameObject.FindGameObjectsWithTag("Tower").ToList();
    }

    private void Update()
    {
        if (touchHandler.Type != TouchHandler.TouchType.None && touchHandler.IsEnded)
        {
            Debug.Log(touchHandler.Type);
        }

        SelectUnits();
    }

    private GameObject SelectTower()
    {
        Vector2 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchHandler.Position);
        RaycastHit2D hit = Physics2D.Raycast(touchWorldPosition, Vector2.zero);

        if (hit.collider != null && towers.Contains(hit.collider.gameObject))
        {
            Debug.Log(hit.collider.gameObject);
            Debug.DrawLine(hit.collider.bounds.min, hit.collider.bounds.max,Color.red,1);
            return hit.collider.gameObject;
        }

        return null;
    }

    private void SelectUnits()
    {
        if (touchHandler.Type == TouchHandler.TouchType.FastClick && touchHandler.IsEnded)
        {
            GameObject newSelectedTower = SelectTower();

            if (selectedTower == null)
            {
                selectedTower = newSelectedTower;
            }
            else if (selectedTower != newSelectedTower)
            {
                if (newSelectedTower != null)
                {
                    // Вычисляем количество юнитов на основе длительности нажатия
                    int unitCount = Mathf.FloorToInt(10);
                    selectedTower.GetComponent<Tower>().ReleaseUnits(unitCount, newSelectedTower);
                }
                selectedTower = null;
            }
            else
            {
                selectedTower = null;
            }
        }
    }
}
