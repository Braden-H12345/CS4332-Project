using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAdd : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            Health test = other.GetComponent<Health>();
            if(other.GetComponent<PlayerMovement>().getGrounded())
            {
                test.takeDamage(damage);
                if (other.GetComponent<DotEffect>() == null)
                {
                    test.gameObject.AddComponent<Stunned>();
                }
            }
        }
    }
}
