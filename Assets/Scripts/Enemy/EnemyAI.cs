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
    [SerializeField] float turnSpeed = 5f;
    NavMeshAgent navMeshAgent;
    EnemyHealth enemyHealth;
    bool isProvoked;
    float distance;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        isProvoked = false;
        CalculateDistance();
    }

    void Update()
    {
        if(enemyHealth.IsDead){
            navMeshAgent.enabled = false;
            this.enabled = false;
            return;
        }
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
            SetIsProvoked(true);
        }
    }

    public void SetIsProvoked(bool isProvoked)
    {
        this.isProvoked = isProvoked;
    }

    void MoveAgent()
    {
        FaceTarget();
        if (distance > navMeshAgent.stoppingDistance)
        {
            FollowTarget();
        }
        if (distance <= navMeshAgent.stoppingDistance)
        {
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
        GetComponent<Animator>().SetBool("Attack", true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sensingRadius);
    }

    private void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
