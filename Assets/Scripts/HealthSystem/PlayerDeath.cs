using UnityEngine;
using System.Collections;

/// <summary>
/// Handles player death events
/// </summary>
public class PlayerDeath : MonoBehaviour
{
    public Health health;
    public Animator animator;
    public float respawnDelay = 3f;
    public int respawnsAvailable = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        Debug.Log("Player has died.");

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Death animation, disable character control, etc..
        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (respawnsAvailable <= 1)
        {
            health.Heal(health.maxHealth);
            // transform.position = Vector3.zero;
            respawnsAvailable--;
            Debug.Log("Player respawned.");
        }
        else if (respawnsAvailable > 1)
        {
            Debug.Log("You have no respawns left. Game Over.");
            // End Game logic and UI
        }
    }
}
