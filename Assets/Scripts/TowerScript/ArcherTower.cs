using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ArcherTower : Tower
{
    List<GameObject> enemiesInRange; // SerializeField вызывает ошибку, потому что когда объекты удаляются, UnityEditor всё равно пытается к ним обратится :/
    [SerializeField] CircleCollider2D RangeOfAttack;
    [SerializeField] GameObject projectile;
    [SerializeField] float attackRate;
    [SerializeField] float attackTimer;
    [SerializeField] float projSpeed;
    [SerializeField] bool IsFire;
    [SerializeField] TargetType type;

    enum TargetType
    {
        First = 0,
        Last,
        Nearest,
        Farthest
    }
    GameObject SelectTarget(TargetType type)
    {
        switch (type)
        {
            case TargetType.First:
                return enemiesInRange.First();

            case TargetType.Last:
                return enemiesInRange.Last();

            case TargetType.Nearest:
                return enemiesInRange.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).First();

            case TargetType.Farthest:
                return enemiesInRange.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).Last();

            default: return null;
        }
    }
    void SpawnProjectile(GameObject target)
    {
        Vector2 targetCenter = target.GetComponent<BoxCollider2D>().bounds.center;
        Vector2 towerCenter = (Vector2)transform.position + RangeOfAttack.offset;
        Vector2 direction = (targetCenter - towerCenter).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        var proj = Instantiate(projectile, towerCenter, Quaternion.Euler(0, 0, angle));
        proj.GetComponent<Rigidbody2D>().velocity = direction * projSpeed;
        proj.GetComponent<Projectile>().owner = gameObject;
    }

    private void Awake()
    {
        RangeOfAttack = GetComponent<CircleCollider2D>() ?? gameObject.AddComponent<CircleCollider2D>();
        enemiesInRange = new List<GameObject>();
    }

    #region Find enemies in range of attack
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

    #endregion


    private void Update()
    {

        if (enemiesInRange.Count != 0)
        {
            attackTimer += Time.deltaTime / attackRate;
            while (attackTimer > 1)
            {
                var target = SelectTarget(type);
                SpawnProjectile(target);
                attackTimer--;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (RangeOfAttack != null) Handles.DrawWireDisc(transform.position + (Vector3)RangeOfAttack.offset, Vector3.forward, RangeOfAttack.radius);
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