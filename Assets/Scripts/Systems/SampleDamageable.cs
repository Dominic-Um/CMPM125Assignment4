using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleDamagable : MonoBehaviour, IDamageable
{
    private int currentHealth;
    public int CurrentHealth => currentHealth;

    [SerializeField] private int maxHealth;
    public int MaxHealth => maxHealth;

    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Heal(int health)
    {}

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
