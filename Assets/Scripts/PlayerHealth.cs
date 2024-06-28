using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    void Start()
    {
    }
    public void ReduceHealth(float damage){
        health -= damage;
        if(health <= 0){
            
        }
    }

}
