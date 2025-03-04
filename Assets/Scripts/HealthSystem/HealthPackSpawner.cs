using UnityEngine;
public class HealthPackSpawner : MonoBehaviour
{
    public string healthPackTag = "HealthPack";
    public Transform[] spawnPoints;
    public float spawnInterval = 10f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnHealthPack), 2f, spawnInterval);
    }

    private void SpawnHealthPack()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        ObjectPooler.Instance.SpawnFromPool(healthPackTag, spawnPoint.position, Quaternion.identity);
    }
}
