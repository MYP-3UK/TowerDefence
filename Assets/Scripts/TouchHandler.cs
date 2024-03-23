using System.Collections;
using UnityEngine;
public class TouchHandler : MonoBehaviour
{
    public enum TouchType
    {
        None = 0,
        FastClick,
        LongClick,
        DoubleClick,
        Drag
    }

    public Vector2 position;
    public TouchType type;
    public float touchTime;
    private int touchCount;
    private bool isCoroutineExecuting = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            position = touch.position;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchTime = Time.time;
                    touchCount++;
                    if (touchCount == 1 && !isCoroutineExecuting)
                    {
                        StartCoroutine(WaitForSecondClick());
                    }
                    break;

                case TouchPhase.Stationary:
                    if (Time.time - touchTime > 0.5f)
                    {
                        type = TouchType.LongClick;
                    }
                    break;

                case TouchPhase.Moved:
                    type = TouchType.Drag;
                    break;
            }
        }
    }

    IEnumerator WaitForSecondClick()
    {
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(0.2f);
        if (touchCount == 2)
        {
            type = TouchType.DoubleClick;
        }
        else if (touchCount == 1)
        {
            type = TouchType.FastClick;
        }
        touchCount = 0;
        isCoroutineExecuting = false;
    }
}
