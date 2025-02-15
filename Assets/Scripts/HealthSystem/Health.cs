using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvincible = false;

    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Applies damage to the entity
    /// </summary>
    /// <param name="amount"> Amount of damage to be applied </param>
    public void TakeDamage(float amount)
    {
        if (isInvincible || currentHealth <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
        Debug.Log($"{amount} damage taken. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Debug.Log($"{gameObject.name} has died.");
    }

    /// <summary>
    /// Heals the entity
    /// </summary>
    /// <param name="amount"> Amount of healing to apply </param>
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
        Debug.Log($"{amount} healed. Current health: {currentHealth}");
    }

    /// <summary>
    /// Temporarily prevents damage ( I-frames )
    /// </summary>
    /// <param name="duration"> Duration of I-Frames </param>
    public void SetInvincibilty(float duration)
    {
        StartCoroutine(InvincibilityRoutine(duration));
    }

    private IEnumerator InvincibilityRoutine(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }
}
