using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    EnemyAI enemyAI;
    bool isDead = false;
    public bool IsDead{get{return isDead;}}
    public float Health{get{return health;}}

    void Start(){
        enemyAI = GetComponent<EnemyAI>();
    }
    public void ReduceHealth(float damage){
        health -= damage;
        enemyAI.SetIsProvoked(true);
        if(health <= 0){
            Die();
            isDead = true;
        }
    }

    void Die(){
        if(isDead) return;
        GetComponent<Animator>().SetTrigger("Die");
    }
}
