using UnityEngine;

/// <summary>
/// Handles damage input for objects with a Health component
/// </summary>
public class Damageable : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    public void ApplyDamage(Damage damage)
    {
        if (health != null)
        {
            health.TakeDamage(damage.damageAmount);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("EnemyProjectile"))
        {
            Damage damage = new Damage(DamageType.Physical, 10); // Dummy Dmg
            ApplyDamage(damage);

            Destroy(collision.gameObject); // Destroy projectile
        }
    }

    public enum DamageType { Physical, Magical, Fire, Poison };

    [System.Serializable]
    public class Damage
    {
        public DamageType damageType;
        public int damageAmount;

        public Damage(DamageType type, int amount)
        {
            damageType = type;
            damageAmount = amount;
        }
    }
}
