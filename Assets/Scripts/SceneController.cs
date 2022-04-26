using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Boss;

    static int currentScene = 0;
    private Health health;
    void Start()
    {
        health = Boss.GetComponent<Health>();

        OnEnable();
    }

    private void OnEnable()
    {
        health.BossKilled += Kill;
    }

    
    void Kill(bool test)
    {
        currentScene ++;
        SceneManager.LoadScene(currentScene);
    }

    private void OnDisable()
    {
        health.BossKilled -= Kill;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
