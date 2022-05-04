using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotEffect : MonoBehaviour
{
    private int applyDamageNTimes = 6;
    private float applyDamageEveryNSeconds = .5f;
    private int appliedTimes = 0;

    private Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = this.gameObject.GetComponent<Health>();
        StartCoroutine(DamageOverTime());
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DamageOverTime()
    {
        while(appliedTimes < applyDamageNTimes)
        {
            health.takeDamage(10);
            yield return new WaitForSeconds(applyDamageEveryNSeconds);
            appliedTimes++;
        }
        Destroy(this);
    }
}
