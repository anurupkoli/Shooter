using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;
    [System.Serializable]
    private class AmmoSlot{
        public AmmoType ammoType;
        public int ammoAmount;
    }

    private AmmoSlot GetAmmoSlot(AmmoType type){
        foreach(AmmoSlot ammoSlot in ammoSlots){
            if(ammoSlot.ammoType == type) return ammoSlot;
        }

        return null;
    }

    public int GetAmmoAmount(AmmoType type){
        return GetAmmoSlot(type).ammoAmount;
    }

    public void ReduceAmmo(AmmoType type){
        GetAmmoSlot(type).ammoAmount--;
    }

    public void IncreaseAmmo(AmmoType type, int amount){
        GetAmmoSlot(type).ammoAmount += amount;
    }
}
