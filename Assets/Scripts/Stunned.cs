using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunned : MonoBehaviour
{
    private GameObject StunnedVisuals;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StunnedEffect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StunnedEffect()
    {
        PlayerMovement move = GetComponent<PlayerMovement>();
        move.setStunned(true);
        yield return new WaitForSeconds(1f);
        move.setStunned(false);
        Destroy(this);
    }
}
