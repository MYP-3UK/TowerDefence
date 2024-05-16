using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private LineRenderer lineRenderer;
    private bool isRendered;
    private Vector3 targetPos;
    [SerializeField] Gradient EfficiencyGradient;

    private void Awake()
    {
        UpdateListOfTowers();
        mask = LayerMask.NameToLayer("Towers");

        lineRenderer = GetComponent<LineRenderer>();
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
                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    if (selectedTower != null && Time.time - StartPressTime > TimeForDetection)
                    {
                        if (!isLongPressing)
                        {
                            isLongPressing = true;
                            StartCoroutine(ReleaseUnitsOverTime());
                        }
                        else
                        {
                            if (SelectTower(touch.position) != selectedTower)
                            {
                                targetTower = SelectTower(touch.position);
                            }
                        }
                    }
                    break;
            }
        }

        DrawSelectionLine();
    }

    private IEnumerator ReleaseUnitsOverTime()
    {
        while (isLongPressing)
        {
            if (selectedTower != targetTower && targetTower != null)
            {
                selectedTower.GetComponent<Tower>().ReleaseUnits(1, targetTower);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private GameObject SelectTower(Vector2 screenPosition)
    {

        Vector2 touchWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        var hit = Physics2D.RaycastAll(touchWorldPosition, Vector3.forward).FirstOrDefault(x => x.collider is BoxCollider2D);

        if (hit.collider != null &&( hit.collider.gameObject.CompareTag("Tower") || hit.collider.gameObject.CompareTag("Base")))
        {
            Debug.Log(hit.collider.gameObject);
            Debug.DrawLine(hit.collider.bounds.min, hit.collider.bounds.max, Color.red, 1);
            return hit.collider.gameObject;
        }

        return null;
    }

    #region GUI
    void DrawSelectionLine()
    {
        if (lineRenderer != null)
        {
            if (selectedTower != null && targetTower != null)
            {
                if (!isRendered)
                {
                    isRendered = true;
                    targetPos = selectedTower.transform.position;
                }
                else
                {
                    targetPos = Vector3.Lerp(targetTower.transform.position, targetPos, 0.5f);
                }

                Vector3 start = selectedTower.transform.position;
                Vector3 height1 = Vector3.up * selectedTower.GetComponent<BoxCollider2D>().bounds.size.y;
                float startWidth = Mathf.Clamp((float)selectedTower.GetComponent<Tower>().GetUnitsCount() / 10f, min: 0.1f, max: 2f);
                Color startColor = EfficiencyGradient.Evaluate(selectedTower.GetComponent<Tower>().CalcEfficiency());

                Vector3 end = targetPos;
                Vector3 height2 = Vector3.up * targetTower.GetComponent<BoxCollider2D>().bounds.size.y;
                float endWidth = Mathf.Clamp((float)targetTower.GetComponent<Tower>().GetUnitsCount() / 10f, min: 0.1f, max: 2f);
                Color endColor = EfficiencyGradient.Evaluate(targetTower.GetComponent<Tower>().CalcEfficiency());


                DrawBezier(start + height1, end + height2, ((start + height1 + end + height2) / 2f) + (Vector3.up * 2f), startColor, endColor, startWidth, endWidth, 64);

            }
            else if (selectedTower != null)
            {
                Vector3 start = selectedTower.transform.position;
                Vector3 height1 = Vector3.up * selectedTower.GetComponent<BoxCollider2D>().bounds.size.y;
                float startWidth = Mathf.Clamp((float)selectedTower.GetComponent<Tower>().GetUnitsCount() / 10f, min: 0.1f, max: 2f);
                Color startColor = EfficiencyGradient.Evaluate(selectedTower.GetComponent<Tower>().CalcEfficiency());

                DrawDot(start + height1, startColor, startWidth);
            }
            else
            {

                isRendered = false;
                ResetBezier();
            }
        }
    }
    void DrawBezier(Vector3 startPosition, Vector3 endPosition, Vector3 startTangent, Vector3 endTangent, Color startColor, Color endColor, float startWidth, float endWidth, int count)
    {
        lineRenderer.positionCount = count;
        lineRenderer.startWidth = startWidth + 0.1f;
        lineRenderer.endWidth = endWidth + 0.1f;
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;


        for (int i = 0; i < count; i++)
        {
            float t = i / ((float)count - 1);

            Vector3 v1 = Vector3.Lerp(startPosition, startTangent, t);
            Vector3 v2 = Vector3.Lerp(startTangent, endTangent, t);
            Vector3 v3 = Vector3.Lerp(endTangent, endPosition, t);

            Vector3 v4 = Vector3.Lerp(v1, v2, t);
            Vector3 v5 = Vector3.Lerp(v2, v3, t);

            Vector3 v6 = Vector3.Lerp(v4, v5, t);

            lineRenderer.SetPosition(i, v6);
        }
    }
    void DrawBezier(Vector3 startPosition, Vector3 endPosition, Vector3 tangent, Color startColor, Color endColor, float startWidth, float endWidth, int count)
    {
        lineRenderer.positionCount = count;
        lineRenderer.startWidth = startWidth + 0.1f;
        lineRenderer.endWidth = endWidth + 0.1f;
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;


        for (int i = 0; i < count; i++)
        {
            float t = i / ((float)count - 1);

            Vector3 v1 = Vector3.Lerp(startPosition, tangent, t);
            Vector3 v2 = Vector3.Lerp(tangent, endPosition, t);

            Vector3 v3 = Vector3.Lerp(v1, v2, t);

            lineRenderer.SetPosition(i, v3);
        }
    }


    void DrawDot(Vector3 pos, Color color, float Width)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = Width;
        lineRenderer.endWidth = Width;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.SetPosition(0, pos);
        lineRenderer.SetPosition(1, pos + (Vector3.up * 0.01f));
    }
    void ResetBezier()
    {
        lineRenderer.positionCount = 0;
    }
    #endregion
}
