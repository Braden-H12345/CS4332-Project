using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            GameObject collider = FindObjectOfType<PlayerMovement>().gameObject;


            if (collider.GetComponent<PlayerMovement>() != null)
            {
                IDamageable dmgPlayer = collider.GetComponent<IDamageable>();

                dmgPlayer.takeDamage(15);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject collider = FindObjectOfType<BossIdentifier>().gameObject;


            if (collider.GetComponent<BossIdentifier>() != null)
            {
                IDamageable dmgPlayer = collider.GetComponent<IDamageable>();

                dmgPlayer.takeDamage(150);
            }
        }
    }
}
