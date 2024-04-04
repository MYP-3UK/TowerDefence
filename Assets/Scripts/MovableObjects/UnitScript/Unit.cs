using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MovableObject
{
    [Header("Animation coefs")]
    [SerializeField] private AnimationCurve jigglingCoef; //Частота шага в зависимости от скорости
    [SerializeField] private AnimationCurve jigglingAmp;  //Амплитуда покачиваний во время ходьбы
    [Header("Characteristics")]
    [SerializeField] private float health = 1f;
    [SerializeField] private float distanceToEnter = 1f;  //Дистанция при которой юнит попадает в здание
    [Header("Active effects")]
    private List<Effect> effects = new List<Effect>();

    private float stepTime; // Фаза ходьбы 
    new void Awake()
    {
        base.Awake(); // Вызов Awake из MovableObject
        stepTime = Random.value; //Рандомная фаза шага (для того, чтобы все юниты не шли в одну ногу)
    }

    new void Update()
    {
        base.Update(); // Вызов Update из MovableObject

        if (Target != null)
        {
            ChangeSpeed();
            ApplyJiggling();

            if (Vector3.Distance(Target.transform.position, transform.position) < distanceToEnter)
            {
                Target.GetComponent<Tower>().EnterUnit(gameObject);
            }
        }
        else
        {
            FindNewTarget();
        }
    }
    
    void FindNewTarget()
    {
        SetTarget(GameObject.FindGameObjectsWithTag("Tower").OrderBy(x => (x.transform.position - transform.position).magnitude).FirstOrDefault());
    }

    void ApplyJiggling()
    {
        stepTime += Time.deltaTime * jigglingCoef.Evaluate(agent.velocity.magnitude);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(stepTime * Mathf.PI) * jigglingAmp.Evaluate(agent.velocity.magnitude));
    }

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

}
