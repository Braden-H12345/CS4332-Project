using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject DeathScreen;

    static int currentScene = 0;
    private Health bossHealth;
    private Health playerHealth;
    void Start()
    {
        bossHealth = Boss.GetComponent<Health>();
        playerHealth = Player.GetComponent<Health>();
        OnEnable();
    }

    private void OnEnable()
    {
        bossHealth.BossKilled += Kill;
        playerHealth.PlayerKilled += PlayerKill;
    }

    
    void Kill(bool test)
    {
        currentScene ++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void PlayerKill(bool test)
    {
        Cursor.lockState = CursorLockMode.None;
        DeathScreen.SetActive(true);
    }

    private void OnDisable()
    {
        bossHealth.BossKilled -= Kill;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.activeSelf == false)
            {
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReloadCurrentScene()
    { 
        currentScene = SceneManager.GetActiveScene().buildIndex; 

        SceneManager.LoadScene(currentScene);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
