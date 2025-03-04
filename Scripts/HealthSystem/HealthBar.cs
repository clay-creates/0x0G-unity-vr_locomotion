using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Updates health UI dynamically
/// </summary>
public class HealthBar : MonoBehaviour
{
    public Health health;
    public Slider healthSlider;

    private void Start()
    {
        // Set slider min and max values
        healthSlider.minValue = 0;
        healthSlider.maxValue = health.maxHealth;

        health.OnHealthChanged += UpdateHealthBar;
        UpdateHealthBar(health.currentHealth);
    }

    private void UpdateHealthBar(float currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}
