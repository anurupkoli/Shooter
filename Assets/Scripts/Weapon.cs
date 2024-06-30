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
    [SerializeField] float timeBetweenShots = 0f;
    PlayerInput playerInput;
    FirstPersonController fpsController;
    InputAction fire;
    InputAction zoom;
    GameObject destroyables;
    Ammo ammo;
    bool isZoomedIn = false;
    bool canShoot = true;
    float zoomOutMouseSensitivity;

    void OnEnable() {
        fpsController = FindObjectOfType<FirstPersonController>();  
        zoomOutMouseSensitivity = fpsController.RotationSpeed; 
        playerInput = FindAnyObjectByType<PlayerInput>(); 
        destroyables = GameObject.FindGameObjectWithTag("Destroyables");
        canShoot = true;
        isZoomedIn = false;
        SetFieldOfView(zoomOutFov);
        SetSensitivity(zoomOutMouseSensitivity);
    }

    void Start()
    {
        ammo = GetComponent<Ammo>();
        fire = playerInput.actions["fire"];
        zoom = playerInput.actions["zoom"];
    }
    void Update()
    {
        if (canShoot && fire.triggered )
        {
            StartCoroutine(Shoot());
        }

        ZoomInAndOut();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammo.AmmoAmount() > 0)
        {
            PlayMuzzleFlash();
            HandleRaycast();
            ammo.ReduceAmmo();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
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
                // Debug.Log(hit.distance);
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
                SetFieldOfView(zoomInFov);
                SetSensitivity(zoomInMouseSensitivity);
            }
            else
            {
                isZoomedIn = false;
                SetFieldOfView(zoomOutFov);
                SetSensitivity(zoomOutMouseSensitivity);
            }
        }
    }

    void SetFieldOfView(float fovAmount){
        cinemachineCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = fovAmount;
    }

    void SetSensitivity(float zoomSensitivity){
        fpsController.RotationSpeed = zoomSensitivity;
    }
}
