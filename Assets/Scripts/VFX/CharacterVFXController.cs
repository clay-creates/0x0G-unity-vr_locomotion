using UnityEngine;

public class CharacterVFXController : MonoBehaviour
{
    [Header("VFX Prefabs")]
    public GameObject reloadVFXPrefab;
    public GameObject sprintVFXPrefab;

    private GameObject currentVFX;

    public void PlayReloadVFX()
    {
        if (reloadVFXPrefab != null)
        {
            currentVFX = Instantiate(reloadVFXPrefab, transform.position, Quaternion.identity);
            Destroy(currentVFX, 6f); // Auto-destroy after 6 seconds
        }
    }

    public void PlaySprintVFX()
    {
        if (sprintVFXPrefab != null)
        {
            currentVFX = Instantiate(sprintVFXPrefab, transform.position, Quaternion.identity);
        }
    }

    public void StopSprintVFX()
    {
        if (currentVFX != null)
        {
            Destroy(currentVFX);
        }
    }
}
