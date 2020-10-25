using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class MoveAgent : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    Transform[] points;
    Transform targetPoint;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetPoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
            SetPoint();
    }

    void SetPoint()
    {
        var newPoint = points.OrderBy(el => Random.Range(1, points.Length)).Take(1).First();
        if (newPoint == targetPoint)
        {
            SetPoint();
            return;
        }
        targetPoint = newPoint;
        agent.SetDestination(targetPoint.position);
    }
}
