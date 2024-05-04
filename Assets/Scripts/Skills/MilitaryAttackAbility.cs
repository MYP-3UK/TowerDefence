using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class MilitaryAttackAbility : Ability
{
    [SerializeField] float damage;

    public override void Activate(GameObject target, GameObject owner)
    {
        if (target != null && !target.IsDestroyed())
        {
            target.GetComponent<MovableObject>().ApplyDamage(damage);
        }
    }
}
