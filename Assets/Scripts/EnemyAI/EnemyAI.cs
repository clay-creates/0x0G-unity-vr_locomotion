using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    public Transform player;
    public NavMeshAgent agent;
    public float chaseRange = 15f;
    public float attackRange = 2f;
    //public float rangedAttackRange = 10f;
    public float attackCooldown = 2f;

    [Header("Combat Settings")]
    public int meleeDamage = 10;
    //public int rangedDamage = 5;
    //public GameObject projectilePrefab;
    //public Transform firePoint;
    //public float projectileSpeed = 10f;

    private float lastAttackTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
            {
                MeleeAttack();
            }
            //else if (distanceToPlayer <= rangedAttackRange && Time.time > lastAttackTime + attackCooldown)
            //{
            //    RangedAttack();
            //}
        }
    }

    void MeleeAttack()
    {
        lastAttackTime = Time.time;
        Debug.Log("Enemy melee attack!");

        if (player.TryGetComponent(out Health playerHealth))
        {
            playerHealth.TakeDamage(meleeDamage);
        }
    }

    //void RangedAttack()
    //{
    //    lastAttackTime += Time.time;
    //    Debug.Log("Enemy ranged attack!");

    //    GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    //    Rigidbody rb = projectile.GetComponent<Rigidbody>();

    //    if (rb != null)
    //    {
    //        Vector3 direction = (player.position - firePoint.position).normalized;
    //        rb.angularVelocity = direction * projectileSpeed;
    //    }
    //}
}
