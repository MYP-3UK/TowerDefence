using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static CustomTools;

public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy properties")]
    [SerializeField] GameObject target;
    [Header("Animation coefs")]
    [SerializeField] AnimationCurve jigglingCoef;
    [SerializeField] AnimationCurve jigglingAmp;
    [Header("Characteristics")]
    [SerializeField] float Health = 1f;
    [SerializeField] float BaseSpeed = 1f;
    [SerializeField] float DistanceToEnter = 1f;

    NavMeshAgent agent;

    float StepTime;

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        StepTime = Random.value;
    }
    void Update()
    {
        if (target != null)
        {
            StepTime += Time.deltaTime * jigglingCoef.Evaluate(agent.velocity.magnitude);
            agent.SetDestination(target.transform.position);

            agent.Raycast(agent.transform.position, out var hit);
            agent.speed = BaseSpeed / agent.GetAreaCost(FirstBitIndex(hit.mask));

            transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(StepTime * Mathf.PI) * jigglingAmp.Evaluate(agent.velocity.magnitude));
        }
        else
        {
            target = GameObject.FindGameObjectsWithTag("Base").OrderBy(x => (x.transform.position - transform.position).magnitude).FirstOrDefault();
        }
    }
}
