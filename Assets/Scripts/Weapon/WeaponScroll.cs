using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScroll : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction scroll;
    Weapon[] weapons;
    int weaponIndex = 0;
    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        scroll = playerInput.actions["scroll"];
        weapons = GetComponentsInChildren<Weapon>();
        DeactivateWeapons();
    }

    void Update()
    {
        HandleWeaponChange();
    }

    void DeactivateWeapons()
    {
        for(int i=1; i<weapons.Count(); i++){
            weapons[i].gameObject.SetActive(false);
        }
    }

    void HandleWeaponChange()
    {
        Vector2 scrollAction = scroll.ReadValue<Vector2>();
        if (scrollAction.y > 0)
        {
            ChangeWeaponIndex(1);
        }
        else if (scrollAction.y < 0)
        {
            ChangeWeaponIndex(-1);
        }
    }

    void ChangeWeaponIndex(int updateIndexBy)
    {
        weapons[weaponIndex].gameObject.SetActive(false);
        weaponIndex += updateIndexBy;
        weaponIndex = weaponIndex % 3;
        if(weaponIndex == -1){
            weaponIndex = weapons.Count()-1;
        }
        weapons[weaponIndex].gameObject.SetActive(true);
    }
}
