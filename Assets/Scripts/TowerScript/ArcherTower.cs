using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class ArcherTower : Tower
{
    [SerializeField] CircleCollider2D RangeOfAttack;
    [SerializeField] List<GameObject> enemiesInRange;
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
            for (int i = enemiesInRange.Count - 1; i >= 0; i--)
            {
                GameObject enemy = enemiesInRange[i];
                Debug.DrawLine(enemy.transform.position, transform.position + (Vector3)RangeOfAttack.offset);
                if (Vector3.Distance(enemy.transform.position, transform.position + (Vector3)RangeOfAttack.offset) > RangeOfAttack.radius + 0.5f)
                {
                    enemiesInRange.RemoveAt(i);
                }
            }
        }

        if (enemiesInRange.Count != 0)
        {
            attackTimer += Time.deltaTime / attackRate;
            while (attackTimer > 1)
            {
                var target = SelectTarget(type);

                var proj = Instantiate(projectile, transform.position + (Vector3)RangeOfAttack.offset, quaternion.identity);
                proj.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * projSpeed;
                proj.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(target.transform.position.y-transform.position.y,target.transform.position.x-transform.position.x) * Mathf.Rad2Deg);
                proj.GetComponent<Projectile>().owner = gameObject;
                    
                proj = null;
                attackTimer--;
            }
        }


        //if (enemiesInRange.Count != 0)
        //{
        //    attackTimer += Time.deltaTime / attackRate;

        //    while (attackTimer > 1)
        //    {
        //        var target = enemiesInRange.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).First();

        //        var proj = Instantiate(projectile, transform);
        //        proj.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * projSpeed;
        //        proj.GetComponent<Projectile>().owner = gameObject;

        //        attackTimer--;
        //    }
        //}

        /*if (enemiesInRange.Count != 0) //МНОГО ОШИБОК (но работает)
        {
            if (!IsFire)
            {
                IsFire = true;
                StartCoroutine(ProjectileSpawner());
            }
        }
        else
        {
            if (IsFire)
            {
                IsFire = false;
                StopCoroutine(ProjectileSpawner());
            }
        }*/


    }
    /*
    private IEnumerator ProjectileSpawner()
    {
        while (true)
        {
            GameObject target = new GameObject();
            float dist2 = Mathf.Infinity;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                GameObject enemy = enemiesInRange[i];
                if (enemy!=null && (enemy.transform.position - transform.position).sqrMagnitude < dist2)
                {
                    dist2 = (enemy.transform.position - transform.position).sqrMagnitude;
                    target = enemy;
                }
            }
            if (target == null) yield return null;
            //var enemy = enemiesInRange.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).First();
            var proj = Instantiate(projectile, transform);
            proj.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * projSpeed;
            proj.GetComponent<Projectile>().owner = gameObject;
            yield return new WaitForSeconds(attackRate);
        }
    }*/

    private void OnDrawGizmos()
    {
        if (RangeOfAttack!=null) Handles.DrawWireDisc(transform.position + (Vector3)RangeOfAttack.offset, Vector3.forward, RangeOfAttack.radius);
        if (enemiesInRange.Count != 0)
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