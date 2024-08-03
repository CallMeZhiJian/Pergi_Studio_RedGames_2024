using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Reference to the Game Over panel
    public TextMeshProUGUI scoreText; // Reference to the Score Text
    public TextMeshProUGUI highScoreText; // Reference to the High Score Text
    public GameObject hud; // Reference to the HUD
    private PlayerMovement playerMovement; // Reference to the PlayerMovement script

    void Start()
    {
        // Find the player GameObject and get the PlayerMovement component
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    public void GameOver()
    {
        // Hide the HUD
        hud.SetActive(false);

        // Display the Game Over panel
        gameOverPanel.SetActive(true);

        // Update the score text
        if (playerMovement != null)
        {
            scoreText.text = Mathf.FloorToInt(playerMovement.GetScore()).ToString();
            playerMovement.CheckForHighScore();
            highScoreText.text = Mathf.FloorToInt(playerMovement.GetHighScore()).ToString();
        }

        // Stop the player's movement
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    // Function to restart the game, called by the Restart button
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
