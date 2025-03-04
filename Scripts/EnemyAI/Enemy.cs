using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IPoolable
{
    [Header("AI Settings")]
    public Transform player;
    public NavMeshAgent agent;
    public float chaseRange = 15f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;

    [Header("Combat Settings")]
    public int meleeDamage = 10;

    [Header("Health Settings")]
    public float health = 100f;

    [Header("Animator")]
    public Animator enemyAnimator;

    [Header("VFX & SFX Settings")]
    public GameObject deathVFXPrefab;
    public AudioSource AudioSource;
    public AudioClip damageSFX;

    private float lastAttackTime;
    private bool isDead = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void OnObjectSpawn()
    {
        health = 100f;
        isDead = false;
        gameObject.SetActive(true);

        if (agent != null)
            agent.enabled = true;
    }

    private void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            agent.SetDestination(player.position);
            enemyAnimator.SetBool("IsMoving", agent.velocity.magnitude > 0.1f);

            if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
            {
                MeleeAttack();
            }
        }
        else
        {
            enemyAnimator.SetBool("IsMoving", false);
        }
    }

    private void MeleeAttack()
    {
        enemyAnimator.SetTrigger("Attack");
        lastAttackTime = Time.time;

        if (player.TryGetComponent(out Health playerHealth))
            playerHealth.TakeDamage(meleeDamage);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        health -= amount;

        enemyAnimator.SetTrigger("TakeDamage");
        AudioSource.PlayOneShot(damageSFX);

        if (health > 0)
        {
            Invoke(nameof(ResetToMove), 0.5f);
        }
        else
        {
            Die();
        }
    }

    private void ResetToMove()
    {
        if (!isDead)
        {
            enemyAnimator.SetBool("IsMoving", true);
        }
    }

    private void Die()
    {
        isDead = true;
        agent.isStopped = true;
        agent.enabled = false;
        enemyAnimator.SetTrigger("Die");

        if (deathVFXPrefab != null)
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);

        gameObject.SetActive(false);
        EnemySpawner.Instance.EnemyDefeated();
    }
}
