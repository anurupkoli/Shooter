using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 30;
    [SerializeField] AmmoType ammoType;
    GameObject player;
    PlayerAmmo playerAmmo;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null){
            playerAmmo = player.GetComponent<PlayerAmmo>();
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == player.gameObject.name){
            playerAmmo.IncreaseAmmo(ammoType, ammoAmount);
            Destroy(gameObject);
        }
    }
}
