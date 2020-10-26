using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float health = 30f;
    [SerializeField]
    GameObject parent;
    [SerializeField]
    GameObject target;
    [SerializeField]
    Collider attackCollider;

    NavMeshAgent agent;
    MoveAgent moveAgent;


    void Start()
    {
        agent = parent.GetComponent<NavMeshAgent>();
        moveAgent = parent.GetComponent<MoveAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == target.gameObject)
            agent.SetDestination(target.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target.gameObject)
            moveAgent.SetRandomPoint();
    }
}
