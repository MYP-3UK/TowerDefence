using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;

    public abstract void Activate(GameObject target, GameObject owner);
}
