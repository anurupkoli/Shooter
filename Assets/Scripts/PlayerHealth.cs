using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    DeathHandler deathHandler;
    void Start()
    {
        deathHandler = GetComponent<DeathHandler>();
    }
    public void ReduceHealth(float damage){
        health -= damage;
        if(health <= 0){
            deathHandler.HandleDeath();
        }
    }

}
