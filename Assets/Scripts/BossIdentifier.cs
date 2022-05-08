using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossIdentifier : MonoBehaviour
{
    //This script is just being used to identify the different bosses for some checks
    //As they will likely be different prefabs entirely it makes it easy for us to quickly ID if they are a boss or not
    [SerializeField] BossLines bossInfo;

    [SerializeField] GameObject narrativeArea;
    [SerializeField] Text bossName;
    [SerializeField] Text bossLine;
    private Health health;
    private bool line1 = false;
    private bool line2 = false;
    private bool line3 = false;
    Coroutine _currentDebug;
    private void Start()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if(health.CurrentHealth / health.MaxHealth <= 1 && line1 == false)
        {
            line1 = true;
            StartCoroutine(CreateLine1());
        }

        if ((float)health.CurrentHealth / (float)health.MaxHealth < .6f && line2 == false)
        {
            line2 = true;
            StartCoroutine(CreateLine2());
        }

        if ((float)health.CurrentHealth / (float)health.MaxHealth < .2f && line3 == false)
        {
            line3 = true;
            StartCoroutine(CreateLine3());
        }
    }
    IEnumerator CreateLine1()
    {
        narrativeArea.SetActive(true);
        bossName.text = bossInfo.name;
        bossLine.text = bossInfo.line1;
        yield return new WaitForSeconds(7f);
        narrativeArea.SetActive(false);
    }

    IEnumerator CreateLine2()
    {
        narrativeArea.SetActive(true);
        bossName.text = bossInfo.name;
        bossLine.text = bossInfo.line2;
        yield return new WaitForSeconds(7f);
        narrativeArea.SetActive(false);
    }

    IEnumerator CreateLine3()
    {
        narrativeArea.SetActive(true);
        bossName.text = bossInfo.name;
        bossLine.text = bossInfo.line3;
        yield return new WaitForSeconds(7f);
        narrativeArea.SetActive(false);
    }
}
