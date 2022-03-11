using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    public event Action<int> PlayerDamaged = delegate { };
    public event Action<int> BossDamaged = delegate { };

    [SerializeField] int _maxHealth = 100;
    [SerializeField] ParticleSystem _damageParticles;
    [SerializeField] AudioClip _damageSound;
    [SerializeField] ParticleSystem _killParticles;
    [SerializeField] AudioClip _killSound;


    public bool _isBoss;

    public int MaxHealth
    {
        get => _maxHealth;
    }

    public int CurrentHealth
    {
        get => _currentHealth;
    }

    private int _currentHealth;

    void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        _currentHealth -= damage;
        Feedback();

        if (this.gameObject.GetComponent<IDamageable>() != null)
        {
            if (this.gameObject.GetComponent<PlayerMovement>() != null)
            {
                PlayerDamaged.Invoke(damage);
            }
            else
            {
                BossDamaged.Invoke(damage);
            }
            

        }


        if (_currentHealth <= 0)
        {
            KillFeedback();
            Kill();
        }
    }

    void KillFeedback()
    {
        if (_killParticles != null)
        {
            _killParticles = Instantiate(_killParticles, this.transform.position, Quaternion.identity);
            _killParticles.Play();

        }

        if (_killSound != null)
        {
            AudioHelper.PlayClip2D(_killSound, 1f);
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }

    void Feedback()
    {
        if (_damageParticles != null)
        {
            _damageParticles = Instantiate(_damageParticles, this.transform.position, Quaternion.identity);
            _damageParticles.Play();

        }

        if (_damageSound != null)
        {
            AudioHelper.PlayClip2D(_damageSound, 1f);
        }
    }
}
