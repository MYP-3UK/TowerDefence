using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{
    [SerializeField] private List<GameObject> towers;
    [SerializeField] private GameObject selectedTower;
    [SerializeField] private GameObject targetTower;
    [SerializeField] private int countOfUnitsToExit;

    [SerializeField] private float StartPressTime;
    [SerializeField] private float TimeForDetection = 0.5f;
    [SerializeField] private bool isLongPressing = false;
    [SerializeField] private int mask;

    private void Awake()
    {
        UpdateListOfTowers();
        mask = LayerMask.NameToLayer("Towers");
    }

    private void UpdateListOfTowers()
    {
        towers = new List<GameObject>();
        towers = GameObject.FindGameObjectsWithTag("Tower").ToList();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    selectedTower = SelectTower(touch.position);
                    StartPressTime = Time.time;
                break;
                case TouchPhase.Ended:
                    isLongPressing = false;
                    selectedTower = null;
                    targetTower = null;
                break;
                case TouchPhase.Stationary: case TouchPhase.Moved:
                    if (selectedTower != null && Time.time - StartPressTime > TimeForDetection)
                    {
                        if (!isLongPressing)
                        {
                            isLongPressing = true;
                            StartCoroutine(ReleaseUnitsOverTime());
                        }
                        else
                        {
                            targetTower = SelectTower(touch.position);
                        }
                    }
                break;
            }
                
            
            //if (touch.phase == TouchPhase.Began)
            //{
            //    longPressStart = Time.time;
            //    Debug.DrawRay(touch.position, Vector3.up);
            //    selectedTower = SelectTower(touch.position);
            //}
            //else if (touch.phase == TouchPhase.Ended)
            //{
            //    isLongPressing = false;
            //    selectedTower = null;
            //}
            //else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            //{
            //    if (selectedTower != null && Time.time - longPressStart > detectLongPressAfter)
            //    {
            //        if (!isLongPressing)
            //        {
            //            isLongPressing = true;
            //            StartCoroutine(ReleaseUnitsOverTime(touch.position));
            //        }
            //    }
            //}
        }
    }
    private IEnumerator ReleaseUnitsOverTime()
    {
        while (isLongPressing)
        {
            if (selectedTower != targetTower && targetTower!=null)
            {
                selectedTower.GetComponent<Tower>().ReleaseUnits(1, targetTower);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private GameObject SelectTower(Vector2 screenPosition)
    {
        
        Vector2 touchWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        var hit = Physics2D.RaycastAll(touchWorldPosition, Vector3.forward).FirstOrDefault(x=>x.collider is BoxCollider2D);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Tower"))
        {
            Debug.Log(hit.collider.gameObject);
            Debug.DrawLine(hit.collider.bounds.min, hit.collider.bounds.max, Color.red, 1);
            return hit.collider.gameObject;
        }

        return null;
    }
}
