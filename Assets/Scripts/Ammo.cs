using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{   
    [SerializeField] AmmoType ammoType;
    PlayerAmmo playerAmmo;

    void Start(){
        playerAmmo = GetComponentInParent<PlayerAmmo>();
    }

    public int AmmoAmount(){
        return playerAmmo.GetAmmoAmount(ammoType);
    }

    public void ReduceAmmo(){
        playerAmmo.ReduceAmmo(ammoType);
    }

    public void IncreaseAmmo(){
        playerAmmo.IncreaseAmmo(ammoType, 30);
    }
}
