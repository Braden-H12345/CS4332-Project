using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    public event Action<int> PlayerDamaged = delegate { };
    public event Action<int> BossDamaged = delegate { };
    public event Action<bool> BossKilled = delegate { };
    public event Action<bool> PlayerKilled = delegate { };

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

    public void takeDamage(int damage)
    {
        Feedback();

        if (this.gameObject.GetComponent<IDamageable>() != null)
        {
            if (this.gameObject.GetComponent<PlayerMovement>() != null)
            {
                Inventory test = this.gameObject.GetComponent<Inventory>();
                Armor testArmor = test.getArmor();
                float armorMod = test.getArmorDmgMod();
                
                if(testArmor != null)
                {
                    int damageRounded = (int)Math.Round(armorMod * damage);

                    Debug.Log("Player Damaged for " + damageRounded + ". Current health: " + _currentHealth);

                    _currentHealth -= damageRounded;

                    PlayerDamaged.Invoke(damageRounded);


                }
                else
                {
                    _currentHealth -= damage;

                    PlayerDamaged.Invoke(damage);
                    Debug.Log("Player Damaged: " + damage);
                    
                }

            }
            else
            {
                _currentHealth -= damage;
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
        if(this.gameObject.GetComponent<BossIdentifier>() != null)
        {
            BossKilled.Invoke(true);
        }
        if(this.gameObject.GetComponent<PlayerMovement>() != null)
        {
            PlayerKilled.Invoke(true);
        }
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
