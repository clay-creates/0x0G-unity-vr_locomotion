using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MenuManager menuManager;
    public int totalZombies = 10;  // Set the number of zombies to spawn
    private int zombiesRemaining;

    private bool playerDead = false;

    void Awake()
    {
        Instance = this;
    }

    public void StartGameplay()
    {
        zombiesRemaining = totalZombies;
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        // Implement your enemy spawning logic here
        Debug.Log("Enemies spawning...");
    }

    public void OnZombieKilled()
    {
        zombiesRemaining--;
        menuManager.AddZombieKill();

        if (zombiesRemaining <= 0)
        {
            EndGame(true); // Player won
        }
    }

    public void OnPlayerDeath()
    {
        if (!playerDead)
        {
            playerDead = true;
            EndGame(false); // Player lost
        }
    }

    private void EndGame(bool playerWon)
    {
        menuManager.EndGame(playerWon);
    }
}
