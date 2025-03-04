using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    public Health health;
    public Animator animator;

    public AudioSource AudioSource;
    public AudioClip deathSFX;

    public float respawnDelay = 3f;
    public int respawnsAvailable = 1;

    void Start()
    {
        health.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        Debug.Log("Player has died.");

        if (animator != null)
            animator.SetTrigger("Die");

        GetComponent<CharacterController>().enabled = false;
        AudioSource.PlayOneShot(deathSFX);

        //StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (respawnsAvailable > 0)
        {
            health.Heal(health.maxHealth);
            respawnsAvailable--;
            Debug.Log("Player respawned.");
        }
        else
        {
            Debug.Log("No respawns left. Game Over.");
        }
    }
}
