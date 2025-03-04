using UnityEngine;
using System.Collections;

/// <summary>
/// Handles player death events
/// </summary>
public class EnemyDeath : MonoBehaviour
{
    public GameObject currentEnemy;
    public Health health;
    public Animator animator;

    public AudioSource AudioSource;
    public AudioClip deathSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        Debug.Log("Enemy has died.");

        // Death animation, disable character control, etc..
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        AudioSource.PlayOneShot(deathSFX);
    }
}
