using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float sensingRadius = 10f;
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;
    bool isProvoked;
    float distance;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        isProvoked = false;
        CalculateDistance();
    }

    void Update()
    {
        CalculateDistance();
        CheckIsProvoked();
        if (isProvoked)
        {
            MoveAgent();
        }
    }

    void CalculateDistance()
    {
        distance = Vector3.Distance(transform.position, target.position);
    }

    void CheckIsProvoked()
    {
        if (distance <= sensingRadius)
        {
            isProvoked = true;
        }
    }

    public void SetIsProvoked(bool isProvoked)
    {
        this.isProvoked = isProvoked;
    }

    void MoveAgent()
    {
        if (distance > navMeshAgent.stoppingDistance)
        {
            FollowTarget();
        }
        if (distance <= navMeshAgent.stoppingDistance)
        {
            GetComponent<Animator>().SetBool("Attack", true);
            AttackTarget();
        }
        else
        {
            GetComponent<Animator>().SetBool("Attack", false);
        }
    }

    void FollowTarget()
    {
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sensingRadius);
    }
}
