using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using static CustomTools;

public class MovableObject : MonoBehaviour
{
    [Header("Target for the navMeshAgent")]
    [SerializeField] private GameObject target;
    [Header("Properties")]
    [SerializeField] private protected float baseSpeed;
    [SerializeField] private protected NavMeshAgent agent;
    public GameObject Target => target;
    public float BaseSpeed => baseSpeed;

    public void SetTarget(GameObject newTarget)
    {
        if (newTarget == null)
        {
            Debug.LogError("New target is null.");
            return;
        }
        target = newTarget;
        agent.SetDestination(target.transform.position);
    }

    private protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>() ?? gameObject.AddComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private protected void OnEnable()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    private protected void Update()
    {
        if (target == null) return;

        if (agent.Raycast(agent.transform.position, out var hit))
        {
            agent.speed = baseSpeed / agent.GetAreaCost(CustomTools.FirstBitIndex(hit.mask));
        }
    }
    private protected void ChangeSpeed()
    {
        agent.Raycast(agent.transform.position, out var hit);
        agent.speed = BaseSpeed / agent.GetAreaCost(FirstBitIndex(hit.mask));
    }
}
