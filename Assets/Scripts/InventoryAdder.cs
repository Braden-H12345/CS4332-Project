using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAdder : MonoBehaviour
{
    [SerializeField] Weapon weapon;

    private void Start()
    {
        GetComponentInParent<Inventory>().setWeapon(weapon);
    }
}
