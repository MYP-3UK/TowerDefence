using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class UnitController : MonoBehaviour
{
    [SerializeField] List<GameObject> towers = new List<GameObject>();
    [SerializeField] TouchHandler touchHandler;
    [SerializeField] GameObject SelectedTower;
    [SerializeField] int CountOfUnitsToExit;
    [SerializeField] Vector2 SelectionBox = new Vector2(0.2f,0.2f);
    
    private void Awake()
    {
        if (GetComponent<TouchHandler>() == null)
        {
            touchHandler = gameObject.AddComponent<TouchHandler>();
        }
        else
        {
            touchHandler = GetComponent<TouchHandler>();
        }
        UpdateListOfTowers();
    }
    void UpdateListOfTowers()
    {
        towers = GameObject.FindGameObjectsWithTag("Tower").ToList();
    }
    void Update()
    {
        
    }
    GameObject SelectTower()
    {
        Vector2 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchHandler.position);
        RaycastHit2D hit = Physics2D.Raycast(touchWorldPosition, Vector2.zero);

        if (towers.Contains(hit.collider.gameObject))
        {
            return SelectedTower = hit.collider.gameObject;
        }
        else { return null; }
    }
}
