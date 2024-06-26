using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    EnemyAI enemyAI;
    public float Health{get{return health;}}

    void Start(){
        enemyAI = GetComponent<EnemyAI>();
    }
    void Update(){
        if(health <= 0){
            DestroyEnemy();
        }
    }
    public void ReduceHealth(float damage){
        health -= damage;
        enemyAI.SetIsProvoked(true);
    }

    void DestroyEnemy(){
        Destroy(gameObject);
    }
}
