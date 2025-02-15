using UnityEngine;
using System.Collections;

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

    private int currentAmmo;
    private bool isReloading = false;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = magazineSize;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading) return;

        if (Input.GetKeyDown("Fire") && currentAmmo > 0)
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.R)) // Manual reload
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        currentAmmo--;

        // Spawn Projectile
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

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

        // Auto reload if mag empty
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        if (audioSource && reloadSound)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;
    }
}
