using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static CustomTools;

public class EnemyBoid : MovableObject
{
    [SerializeField] CircleCollider2D range;
    [SerializeField] List<EnemyBoid> agents;
    [SerializeField] float distanceOfSeparation;
    [SerializeField] float CohesionCoef;
    [SerializeField] float SeparationCoef;
    [SerializeField] float AlignmentCoef;
    [SerializeField] float BoidCoef;
    [SerializeField] float DesiredVelocityCoef;
    [SerializeField] float slowCoef;
    [SerializeField] KeyCode key;

    public Vector2 position;
    public Vector2 velocity;

    private new void Awake()
    {
        base.Awake();
    }

    private new void Update()
    {
        if (Input.GetKey(key))
        {
            agent.SetDestination((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        position = agent.gameObject.transform.position;
        velocity = (Vector2)(agent.desiredVelocity * DesiredVelocityCoef + agent.velocity);
        
        ApplyBoidSim();


        agent.Raycast(agent.transform.position, out var hit);
        agent.velocity = velocity / Mathf.Exp(slowCoef*(agent.GetAreaCost(FirstBitIndex(hit.mask))-1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.CompareTag("Agent"))
        {
            agents.Add(collision.gameObject.GetComponent<EnemyBoid>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.CompareTag("Agent"))
        {
            agents.Remove(collision.gameObject.GetComponent<EnemyBoid>());
        }
    }

    private void ApplyBoidSim()
    {
        Vector2 r1 = Cohesion();
        Vector2 r2 = Separation(distanceOfSeparation);
        Vector2 r3 = Alignment();

        velocity += (r1 * CohesionCoef + 
                     r2 * SeparationCoef +
                     r3 * AlignmentCoef) * BoidCoef;

        velocity = Vector3.ClampMagnitude(velocity, 3);
    }
    private void OnDrawGizmosSelected()
    {
        if (agent.hasPath)
        {
            Debug.Log("HYI");
            for (int i = 0; i < agent.path.corners.Length - 1; i++)
            {
                Vector3 path1 = agent.path.corners[i];
                Vector3 path2 = agent.path.corners[i+1];
                Gizmos.DrawLine(path1, path2);
            }
        }
    }
    Vector2 Cohesion()
    {
        if (agents.Count == 0) return Vector2.zero;

        Vector2 center = Vector2.zero;
        foreach (EnemyBoid agent in agents)
        {
            center += agent.position;
        }
        center /= agents.Count;

        Debug.DrawRay(position, center - position, Color.red);

        return center - position;
    }
    Vector2 Separation(float distanceOfSeparation)
    {
        if (agents.Count == 0) return Vector2.zero;

        Vector2 separation = Vector2.zero;
        foreach (EnemyBoid agent in agents)
        {
            if (Vector2.Distance(agent.position,position) < distanceOfSeparation)
            {
                separation -= (agent.position - position);
            }
        }

        Debug.DrawRay(position, separation, Color.green);

        return separation;
    }
    Vector2 Alignment()
    {
        if (agents.Count == 0) return Vector2.zero;
        Vector2 alignment = Vector2.zero;

        foreach (EnemyBoid agent in agents)
        {
            if (agent.velocity != null)
            {
                alignment += agent.velocity;
            }
        }

        alignment /= agents.Count;
        Debug.DrawRay(position, alignment, Color.blue);

        return alignment;
    }
}
