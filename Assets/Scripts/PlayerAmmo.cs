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

    public int GetAmmoAmount(AmmoType type){
        foreach(AmmoSlot ammoSlot in ammoSlots){
            if(ammoSlot.ammoType == type){
                return ammoSlot.ammoAmount;
            }
        }

        return -1;
    }

    public void ReduceAmmo(AmmoType type){
        foreach(AmmoSlot ammoSlot in ammoSlots){
            if(ammoSlot.ammoType == type){
                ammoSlot.ammoAmount--;
            }
        }
    }

    public void IncreaseAmmo(AmmoType type, int amount){
        foreach(AmmoSlot ammoSlot in ammoSlots){
            if(ammoSlot.ammoType == type){
                ammoSlot.ammoAmount += amount;
            }
        }
    }
}
