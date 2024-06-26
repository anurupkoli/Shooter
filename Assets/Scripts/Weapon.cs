using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 50f;
    PlayerInput playerInput;
    InputAction fire;

    EnemyHealth enemyHealth;
    void Start()
    {
        playerInput = FindAnyObjectByType<PlayerInput>();
        fire = playerInput.actions["fire"];
        enemyHealth = FindAnyObjectByType<EnemyHealth>();
    }
    void Update()
    {
        if (fire.triggered)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform.gameObject == enemyHealth.gameObject)
            {
                enemyHealth.ReduceHealth(damage);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }
}
