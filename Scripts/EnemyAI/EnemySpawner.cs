using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public string enemyTag = "Enemy";
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxEnemies = 10;

    private int currentEnemyCount = 0;

    public static EnemySpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = ObjectPooler.Instance.SpawnFromPool(enemyTag, spawnPoint.position, Quaternion.identity);
        if (enemy != null)
        {
            currentEnemyCount++;
            enemy.GetComponent<Enemy>().OnObjectSpawn();
        }
    }

    public void EnemyDefeated()
    {
        currentEnemyCount--;
    }
}

