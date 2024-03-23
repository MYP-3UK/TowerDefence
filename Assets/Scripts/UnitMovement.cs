using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] AnimationCurve jigglingCoef;
    [SerializeField] AnimationCurve jigglingAmp;
    NavMeshAgent agent;
    
    float StepTime;
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
        agent.SetDestination(target.position);
        transform.rotation = Quaternion.Euler(0,0,Mathf.Sin(StepTime*Mathf.PI) *jigglingAmp.Evaluate(agent.velocity.magnitude));
    }
}
