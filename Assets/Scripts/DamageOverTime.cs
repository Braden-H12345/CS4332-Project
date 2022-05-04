using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    [SerializeField] int damage;
    private float timeElapsed = 0f;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            Health test = other.GetComponent<Health>();
            test.takeDamage(damage);
            if(other.GetComponent<DotEffect>() == null)
            {
                test.gameObject.AddComponent<DotEffect>();
            }
        }
    }
}
