using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;

    float cooldownTime;
    float activeTime;

    bool isActivated;
    GameObject target;

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    public void ActivateAbility(GameObject target)
    {
        isActivated = true;
        this.target = target;
    }

    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                if (isActivated)
                {
                    isActivated = false;
                    ability.Activate(target, gameObject);
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                }
                break;
            case AbilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }
}
