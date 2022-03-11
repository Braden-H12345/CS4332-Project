using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Slider _healthSlider = null;


    //this will be either the boss or player. As both have different max Healths it allows one script to function identically
    public GameObject _objectToTrack;

    private Health Health;

    void Start()
    {
        Health = _objectToTrack.GetComponent<Health>();
        _healthSlider.maxValue = Health.MaxHealth;
        _healthSlider.value = Health.MaxHealth;

        OnEnable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        Health.PlayerDamaged += UpdateUI;

        Health.BossDamaged += UpdateUI;
    }

    void UpdateUI(int damage)
    {
        _healthSlider.value = Health.CurrentHealth;
    }

    private void OnDisable()
    {
        if (_objectToTrack.GetComponent<BossIdentifier>() != null)
        {
            Health.BossDamaged -= UpdateUI;
        }

        if (_objectToTrack.GetComponent<PlayerMovement>() != null)
        {
            Health.PlayerDamaged -= UpdateUI;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
