using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject cinemachineCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 50f;
    [SerializeField] float zoomInFov = 20f;
    [SerializeField] float zoomOutFov = 40f;
    [SerializeField] float zoomInMouseSensitivity = 0.5f;
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject hitSparks;
    PlayerInput playerInput;
    FirstPersonController fpsController;
    InputAction fire;
    InputAction zoom;
    GameObject destroyables;
    Ammo ammo;
    bool isZoomedIn = false;
    float zoomOutMouseSensitivity;

    void Start()
    {
        ammo = GetComponent<Ammo>();
        playerInput = FindAnyObjectByType<PlayerInput>();
        fire = playerInput.actions["fire"];
        zoom = playerInput.actions["zoom"];
        destroyables = GameObject.FindGameObjectWithTag("Destroyables");
        fpsController = FindObjectOfType<FirstPersonController>();
        zoomOutMouseSensitivity = fpsController.RotationSpeed;
    }
    void Update()
    {
        if (fire.triggered)
        {
            Shoot();
        }

        ZoomInAndOut();
    }

    void Shoot()
    {
        if (ammo.AmmoAmount <= 0)
        {
            return;
        }
        ammo.ReduceAmmo();
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

    void PlayHitSparks(RaycastHit hit)
    {
        GameObject hitSpark = Instantiate(hitSparks, hit.point, Quaternion.LookRotation(hit.normal));
        hitSpark.transform.parent = destroyables.transform;
        Destroy(hitSpark, 0.1f);
    }

    void ZoomInAndOut()
    {
        if (zoom.triggered)
        {
            if (!isZoomedIn)
            {
                isZoomedIn = true;
                cinemachineCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = zoomInFov;
                fpsController.RotationSpeed = zoomInMouseSensitivity;
            }
            else
            {
                isZoomedIn = false;
                cinemachineCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = zoomOutFov;
                fpsController.RotationSpeed = zoomOutMouseSensitivity;
            }
        }
    }
}
