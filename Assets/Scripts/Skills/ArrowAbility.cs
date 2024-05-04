using Unity.VisualScripting;
using UnityEngine;
using static CustomTools;

[CreateAssetMenu]
public class ArrowAbility : Ability
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projSpeed;
    [SerializeField] float damage;
    [SerializeField] Team team;

    public override void Activate(GameObject target, GameObject owner)
    {
        if (target != null && !target.IsDestroyed())
        {
            Vector2 targetCenter = target.GetComponent<BoxCollider2D>().bounds.center;
            Vector2 towerCenter = owner.GetComponent<BoxCollider2D>().bounds.center;
            Vector2 direction = (targetCenter - towerCenter).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            var proj = Instantiate(projectile, towerCenter, Quaternion.Euler(0, 0, angle));
            proj.GetComponent<Rigidbody2D>().velocity = direction * projSpeed;
            proj.GetComponent<Projectile>().owner = owner;
            proj.GetComponent<Projectile>().team = team;
            proj.GetComponent<Projectile>().damage = damage;
        }
    }
}
