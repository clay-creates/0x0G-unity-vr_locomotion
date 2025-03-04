using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Canvas Panels")]
    public GameObject startMenu;  // Start Menu Panel
    public GameObject endMenu;    // End Menu Panel

    [Header("Start Menu")]
    public Button startButton;
    public Button quitButton;

    [Header("End Game Menu")]
    public Button replayButton;
    public Button endQuitButton;
    public TMP_Text timeAliveText;
    public TMP_Text damageDealtText;
    public TMP_Text damageHealedText;
    public TMP_Text zombiesKilledText;

    private float gameStartTime;
    private bool gameActive = false;
    private int totalDamageDealt = 0;
    private int totalDamageHealed = 0;
    private int zombiesKilled = 0;

    void Start()
    {
        // Assign button listeners
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        replayButton.onClick.AddListener(RestartGame);
        endQuitButton.onClick.AddListener(QuitGame);

        // Ensure the Start Menu is active and the End Menu is hidden at the start
        startMenu.SetActive(true);
        endMenu.SetActive(false);
    }

    public void StartGame()
    {
        startMenu.SetActive(false); // Hide Start Menu
        gameStartTime = Time.time;  // Start tracking time
        gameActive = true;          // Game is now active

        // Call function to start gameplay loop (enemy spawning, etc.)
        GameManager.Instance.StartGameplay();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        // Reload the scene to reset everything
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame(bool playerWon)
    {
        gameActive = false;

        // Calculate stats
        float timeAlive = Time.time - gameStartTime;
        timeAliveText.text = $"Time Alive: {timeAlive:F2}s";
        damageDealtText.text = $"Damage Dealt: {totalDamageDealt}";
        damageHealedText.text = $"Damage Healed: {totalDamageHealed}";
        zombiesKilledText.text = $"Zombies Killed: {zombiesKilled}";

        // Show End Menu
        endMenu.SetActive(true);
    }

    // These methods can be called by other scripts to update the stats
    public void AddDamageDealt(int amount) => totalDamageDealt += amount;
    public void AddDamageHealed(int amount) => totalDamageHealed += amount;
    public void AddZombieKill() => zombiesKilled++;
}
