using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MovableObject
{
    [Header("Animation coefs")]
    [SerializeField] private AnimationCurve jigglingCoef; //Частота шага в зависимости от скорости
    [SerializeField] private AnimationCurve jigglingAmp;  //Амплитуда покачиваний во время ходьбы

    [Header("Flipping sprite")]
    [SerializeField] private bool isFlipping; //Отражение спрайта в зависимости от направления движения
    [SerializeField] private float velocityForFlipping; //Пороговое значение скорости, при котором меняется направление

    [Header("Characteristics")]
    [SerializeField] private float health = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float radiusOfAttack = 1f;
    [SerializeField] private float distanceToEnter = 1f; //Дистанция при которой враг попадает на базу
    [Header("Active effects")]
    private List<Effect> effects = new List<Effect>();
    public bool isDead;

    private float stepTime; // Фаза ходьбы (для того, чтобы все враги не шли в одну ногу)

    private SpriteRenderer spriteRenderer;

    new void Awake()
    {
        base.Awake(); // Вызов Awake из MovableObject
        stepTime = Random.value;

        spriteRenderer = GetComponent<SpriteRenderer>() ?? gameObject.AddComponent<SpriteRenderer>();
    }

    new void Update()
    {
        base.Update(); // Вызов Update из MovableObject

        if (Target != null)
        {
            ChangeSpeed();
            ApplyJiggling();
        }
        else
        {
            FindNewTarget();
        }

        if (isFlipping && Mathf.Abs(agent.velocity.x) > velocityForFlipping)
        {
            spriteRenderer.flipX = agent.velocity.x > 0;
        }

    }

    void FindNewTarget()
    {
        SetTarget(GameObject.FindGameObjectsWithTag("Base").OrderBy(x => (x.transform.position - transform.position).magnitude).FirstOrDefault());
    }

    void ApplyJiggling()
    {
        stepTime += Time.deltaTime * jigglingCoef.Evaluate(agent.velocity.magnitude);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(stepTime * Mathf.PI) * jigglingAmp.Evaluate(agent.velocity.magnitude));
    }

    public void ApplyDamage(float damage, GameObject owner)
    {
        health -= damage;
        if (health < 0)
        {
            if (owner.CompareTag("Tower"))
            {
                owner.GetComponent<ArcherTower>().RemoveEnemyFromList(gameObject);
            }
            if (owner.CompareTag("Warrior"))
            {
                owner.GetComponent<Warrior>().RemoveEnemyFromList(gameObject);
            }
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    //TODO
    #region Effects 
    public void AddEffect(Effect effect)
    {
        effects.Add(effect);
        effect.Apply();
    }

    public void RemoveEffect(Effect effect)
    {
        effect.Remove();
        effects.Remove(effect);
    }

    public void UpdateEffects()
    {
        foreach (var effect in effects.ToList())
        {
            effect.Duration -= Time.deltaTime;
            if (effect.Duration <= 0)
            {
                RemoveEffect(effect);
            }
        }
    }
    #endregion
}
