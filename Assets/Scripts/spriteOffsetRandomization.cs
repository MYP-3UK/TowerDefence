using UnityEngine;

public class spriteOffsetRandomization : MonoBehaviour
{
    [SerializeField] Vector2 RandomizationScale;
    [SerializeField] Vector2 RandomOffset;
    [SerializeField] float RandomRotation;
    [SerializeField] Gradient Color;
    private void Awake()
    {
        transform.localScale = (Random.insideUnitCircle * RandomizationScale) + Vector2.one;
        transform.Translate(Random.insideUnitCircle * RandomOffset);
        transform.Rotate(0, 0, Random.Range(-RandomRotation, RandomRotation));
        GetComponent<SpriteRenderer>().color = Color.Evaluate(Random.value);
    }
}
