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
    public float smoothSpeed = 0.2f;

    private void Start()
    {
        health.OnHealthChanged += UpdateHealthBar;
        UpdateHealthBar(health.currentHealth);
    }

    private void UpdateHealthBar(float currentHealth)
    {
        StartCoroutine(SmoothHealthChange(currentHealth / health.maxHealth));
    }

    private IEnumerator SmoothHealthChange(float targetValue)
    {
        float elapsedTime = 0f;
        float currentValue = healthSlider.value;

        while (elapsedTime < smoothSpeed)
        {
            elapsedTime += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(currentValue, targetValue, elapsedTime / smoothSpeed);
            yield return null;
        }

        healthSlider.value = targetValue;
    }
}
