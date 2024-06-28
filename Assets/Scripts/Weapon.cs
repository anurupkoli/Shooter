using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 50f;
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject hitSparks;
    PlayerInput playerInput;
    InputAction fire;
    GameObject destroyables;

    void Start()
    {
        playerInput = FindAnyObjectByType<PlayerInput>();
        fire = playerInput.actions["fire"];
        destroyables = GameObject.FindGameObjectWithTag("Destroyables");

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
        PlayMuzzleFlash();
        HandleRaycast();
    }

    void PlayMuzzleFlash()
    {
        muzzleFlashVFX.Play();
    }

    void HandleRaycast()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            PlayHitSparks(hit);
            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
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

    void PlayHitSparks(RaycastHit hit){
        GameObject hitSpark = Instantiate(hitSparks, hit.point, Quaternion.LookRotation(hit.normal));
        hitSpark.transform.parent = destroyables.transform;
        Destroy(hitSpark, 0.1f);
    }
}
