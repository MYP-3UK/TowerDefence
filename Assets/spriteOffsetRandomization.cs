using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteOffsetRandomization : MonoBehaviour
{
    [SerializeField] Vector2 RandomizationScale;
    [SerializeField] Vector2 RandomOffset;
    [SerializeField] Gradient Color;
    private void Awake()
    {
        transform.localScale = Random.insideUnitCircle * RandomizationScale + Vector2.one;
        transform.Translate(Random.insideUnitCircle * RandomOffset);
        GetComponent<SpriteRenderer>().color = Color.Evaluate(Random.value);
    }
}
