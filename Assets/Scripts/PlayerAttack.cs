using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Weapon;
    public float AttackCooldown = 1.0f;
    public bool isAttacking = false;
    private bool CanAttack = true;


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(CanAttack)
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        isAttacking = true;
        CanAttack = false;
        Animator anim = Weapon.GetComponent<Animator>();
        anim.SetTrigger("Attack");

        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(.45f);
        isAttacking = false;
    }
}
