using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScroll : MonoBehaviour
{
    // Start is called before the first frame update
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

    void Update(){
        HandleWeaponChange();
        Debug.Log(weaponIndex);
    }

    void DeactivateWeapons(){
        foreach(Weapon weapon in weapons){
            weapon.gameObject.SetActive(false);
        }
    }

    void HandleWeaponChange(){
        Vector2 scrollAction = scroll.ReadValue<Vector2>();
        if(scrollAction.y > 0){
            weapons[weaponIndex].gameObject.SetActive(false);
            weaponIndex += 1;
            weaponIndex = Mathf.Abs(weaponIndex % 3);
            weapons[weaponIndex].gameObject.SetActive(true);
        }
        else if(scrollAction.y < 0){
            weapons[weaponIndex].gameObject.SetActive(false);
            weaponIndex -= 1;
            weaponIndex = Mathf.Abs(weaponIndex % 3);
            weapons[weaponIndex].gameObject.SetActive(true);
        }
    }
}
