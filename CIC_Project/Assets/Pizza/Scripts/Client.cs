using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Client : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform positionDestination;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();    
        agent.SetDestination(positionDestination.position);
    }

    void Update()
    {
        animator.SetFloat("Move", agent.velocity.magnitude);
    }
}
