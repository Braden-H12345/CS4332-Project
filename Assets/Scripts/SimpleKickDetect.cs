using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleKickDetect : MonoBehaviour
{
    [SerializeField] int damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            Health test = other.GetComponent<Health>();

            test.takeDamage(damage);
        }
    }
}
