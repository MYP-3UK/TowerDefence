using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] AnimationCurve jigglingCoef;
    [SerializeField] AnimationCurve jigglingAmp;
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
        StepTime += Time.deltaTime * jigglingCoef.Evaluate(agent.velocity.magnitude);
        agent.SetDestination(target.transform.position);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(StepTime * Mathf.PI) * jigglingAmp.Evaluate(agent.velocity.magnitude));

        if (Vector3.Distance(target.transform.position, transform.position) < DistanceToEnter)
        {
            target.GetComponent<Tower>().EnterUnit(gameObject);
        };
    }
}
