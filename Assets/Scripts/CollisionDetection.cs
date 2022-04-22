using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public PlayerAttack weapon;

    private int count = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && weapon.isAttacking)
        {
            Health test = other.GetComponent<Health>();
            Inventory inv = FindObjectOfType<Inventory>();
            test.takeDamage(inv.getWeaponDamage());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && weapon.isAttacking && count == 0)
        {
            Health test = other.GetComponent<Health>();
            Inventory inv = FindObjectOfType<Inventory>();
            test.takeDamage(inv.getWeaponDamage());

            //to ensure it only gets damaged once per attack
            count++;
        }

        if(!weapon.isAttacking)
        {
            count = 0;
        }
    }
}
