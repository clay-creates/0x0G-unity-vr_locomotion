using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    public int magazineSize = 30;
    public float reloadTime = 2f;

    [Header("VFX & Audio")]
    public GameObject fireVFX;
    public AudioClip fireSound;
    public AudioClip reloadSound;

    [Header("InputActions")]
    public InputActionProperty fireAction;
    public InputActionProperty reloadAction;

    [Header("UI")]
    public Slider reloadSlider;

    private int currentAmmo;
    private bool isReloading = false;
    private AudioSource audioSource;

    private void OnEnable()
    {
        fireAction.action.Enable();
        fireAction.action.performed += ctx => TryFire();
        reloadAction.action.Enable();
        reloadAction.action.performed += ctx => StartCoroutine(Reload());
    }

    private void OnDisable()
    {
        fireAction.action.Disable();
        reloadAction.action.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = magazineSize;
        audioSource = GetComponent<AudioSource>();

        if (reloadSlider != null )
        {
            reloadSlider.gameObject.SetActive(false);
        }
    }

    private void TryFire()
    {
        if (!isReloading || currentAmmo <= 0) return;

        Shoot();
        currentAmmo--;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload()); // Auto reload when empty
        }
    }

    public void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Spawn Projectile
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }

        // Fire VFX
        if (fireVFX)
        {
            Instantiate(fireVFX, firePoint.position, firePoint.rotation);
        }

        // Play fire audio
        if (audioSource && fireSound)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        if (audioSource && reloadSound)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true);
            reloadSlider.value = 0;
        }

        float elapsedTime = 0f;
        while (elapsedTime < reloadTime)
        {
            elapsedTime += Time.deltaTime;
            if (reloadSlider != null)
            {
                reloadSlider.value = elapsedTime / reloadTime;
            }
            yield return null;
        }

        currentAmmo = magazineSize;
        isReloading = false;

        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(false);
        }
    }
}
