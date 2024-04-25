using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using static CustomTools;

public class Warrior : MovableObject
{
    [SerializeField] public GameObject MotherTower;
    [SerializeField] List<GameObject> enemiesInRange;
    [SerializeField] CircleCollider2D rangeOfVision;

    [Header("Animation coefs")]
    [SerializeField] private AnimationCurve jigglingCoef; //Частота шага в зависимости от скорости
    [SerializeField] private AnimationCurve jigglingAmp;  //Амплитуда покачиваний во время ходьбы
    private float stepTime; // Фаза ходьбы 

    private GameObject _target;

    [Header("Attack stats")]
    [SerializeField] float attackDamage;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackRange;

    [SerializeField] float patrolTime;
    [SerializeField] Vector2 patrolTarget;

    enum State 
    {
        None = 0,
        Stalking,
        Patrolling
    }

    [SerializeField] State state;


    new void Awake()
    {
        state = State.None;
        enemiesInRange = new List<GameObject>();
        rangeOfVision = GetComponent<CircleCollider2D>() ?? gameObject.AddComponent<CircleCollider2D>();
    }

    new void Update()
    {
        ChangeSpeed();


        ApplyJiggling();
        Patrol();
        Attack();
    }
    void ApplyJiggling()
    {
        stepTime += Time.deltaTime * jigglingCoef.Evaluate(agent.velocity.magnitude);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(stepTime * Mathf.PI) * jigglingAmp.Evaluate(agent.velocity.magnitude));
    }

    void Attack()
    {
        var enemiesInPatrolRange = enemiesInRange.
                                    Where(x => (x.transform.position - MotherTower.transform.position).magnitude < MotherTower.GetComponent<WarriorTower>().PatrolDistance);
        if (enemiesInPatrolRange.Count() > 0)
        {
            _target = enemiesInPatrolRange.
                OrderBy(x => (x.transform.position - transform.position).magnitude).
                First();

            SetTarget(_target);
            if (state != State.Stalking)
            {
                state = State.Stalking;
                StartCoroutine(AttackCycle());
            }
        }
        else if(state == State.Stalking)
        {
            
            state = State.None;
            _target = null;
        }
    }

    void Patrol()
    {
        if (enemiesInRange.Count == 0 && (state == State.None||state == State.Stalking))
        {
            state = State.Patrolling;
            StartCoroutine(PatrolCycle());
        }
    }

    IEnumerator AttackCycle()
    {
        while (_target != null && state == State.Stalking)
        {
            if (Vector2.Distance(_target.transform.position, (Vector2)transform.position + rangeOfVision.offset) < attackRange)
            {
                _target.GetComponent<Enemy>().ApplyDamage(attackDamage, gameObject);
            }
            Debug.Log("Attack!");
            yield return new WaitForSeconds(attackSpeed);
        }
    }
    IEnumerator PatrolCycle()
    {
        while (state == State.Patrolling)
        {
            yield return new WaitForEndOfFrame();
            patrolTarget = (Vector2)MotherTower.transform.position + Random.insideUnitCircle * (MotherTower.GetComponent<WarriorTower>().PatrolDistance+1);
            SetTarget(patrolTarget);
            yield return new WaitForSeconds(patrolTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, является ли объект врагом
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Добавляем врага в список
            enemiesInRange.Add(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Проверяем, является ли объект врагом
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Добавляем врага в список
            enemiesInRange.Remove(other.gameObject);
        }
    }
    public void RemoveEnemyFromList(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    private void OnDrawGizmos()
    {
        if (rangeOfVision != null) Handles.DrawWireDisc(transform.position + (Vector3)rangeOfVision.offset, Vector3.forward, rangeOfVision.radius);
        if (attackRange > 0) Handles.DrawWireDisc(transform.position + (Vector3)rangeOfVision.offset, Vector3.forward, attackRange);
        Handles.DrawLine(transform.position, patrolTarget);
        if (enemiesInRange != null && enemiesInRange.Count != 0)
        {
            foreach (GameObject enemy in enemiesInRange)
            {
                if (enemy != null)
                {
                    var collider = enemy.GetComponent<BoxCollider2D>();
                    Handles.DrawWireCube(enemy.transform.position + (Vector3)collider.offset, collider.size);
                }
            }
        }

    }
}
