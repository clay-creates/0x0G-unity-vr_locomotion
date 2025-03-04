using UnityEngine;

public class HealthPack : MonoBehaviour, IPoolable
{
    public float healAmount = 25f;

    public AudioSource AudioSource;
    public AudioClip healSFX;

    public void OnObjectSpawn()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayOneShot(healSFX);

        if (other.CompareTag("Player") && other.TryGetComponent(out Health playerHealth))
        {
            playerHealth.Heal(healAmount);
            gameObject.SetActive(false); // Return to pool
        }
    }
}
