using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;
    public int damage = 10;
    public GameObject impactVFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        if (impactVFX)
        {
            Instantiate(impactVFX, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
