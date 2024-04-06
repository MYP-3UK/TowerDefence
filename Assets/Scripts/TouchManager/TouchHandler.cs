using System;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    [Serializable]
    public enum TouchType
    {
        None = 0,
        FastClick,
        LongClick,
        DoubleClick,
        Drag
    }

    public Vector2 Position;
    public TouchType Type;
    public bool IsTouched;
    public bool IsEnded;

    private const float DoubleClickTimeLimit = 0.2f;
    private const float LongClickTimeLimit = 0.5f;

    private float touchStartTime;
    public float TouchDuration;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Position = touch.position;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartTime = Time.time;
                    IsTouched = true;
                    break;

                case TouchPhase.Moved:
                    Type = TouchType.Drag;
                    break;

                case TouchPhase.Ended:
                    TouchDuration = Time.time - touchStartTime;
                    if (TouchDuration >= LongClickTimeLimit)
                    {
                        Type = TouchType.LongClick;
                    }
                    else
                    {
                        Type = TouchType.FastClick;
                    }
                    IsTouched = false;
                    break;
            }
            IsEnded = touch.phase == TouchPhase.Ended;
        }
        else
        {
            Type = TouchType.None;
        }
    }

}
