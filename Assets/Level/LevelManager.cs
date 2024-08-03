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
    public TextMeshProUGUI coinText; // Reference to the High Score Text
    public GameObject hud; // Reference to the HUD
    private PlayerMovement playerMovement; // Reference to the PlayerMovement script

    public Animator gameOverPanelAnimation;
    private bool isGameOverPanelOpen = false;

    public Animator transitionAnimation;
    private bool isPauseOpen = false;

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

        isGameOverPanelOpen = !isGameOverPanelOpen;
        gameOverPanelAnimation.SetBool("OpenGameOverPanel", isGameOverPanelOpen);

        // Update the score text
        if (playerMovement != null)
        {
            scoreText.text = Mathf.FloorToInt(playerMovement.GetScore()).ToString();
            playerMovement.CheckForHighScore();
            highScoreText.text = Mathf.FloorToInt(playerMovement.GetHighScore()).ToString();
            coinText.text = PlayerPrefs.GetInt("Coins").ToString();
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
        Time.timeScale = 1f;
        Audio_MainMenu.instance.PlaySFX(0);
        Audio_MainMenu.instance.bgmAudioSource1.Stop();
        Audio_MainMenu.instance.bgmAudioSource2.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMainMenu()
    {
        Audio_MainMenu.instance.PlaySFX(0);
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        Audio_MainMenu.instance.PlaySFX(0);
        Audio_MainMenu.instance.bgmAudioSource1.Pause();
        Audio_MainMenu.instance.bgmAudioSource2.Pause();
        isPauseOpen = !isPauseOpen;
        transitionAnimation.SetBool("Transition", isPauseOpen);
        StartCoroutine(PauseTime());
    }

    public void Resume()
    {
        Audio_MainMenu.instance.PlaySFX(0);
        Audio_MainMenu.instance.bgmAudioSource1.UnPause();
        Audio_MainMenu.instance.bgmAudioSource2.UnPause();
        Time.timeScale = 1f;
        isPauseOpen = !isPauseOpen;
        transitionAnimation.SetBool("Transition", isPauseOpen);
    }

    private IEnumerator PauseTime()
    {
        yield return new WaitForSeconds(0.6f);

        Time.timeScale = 0f;
    }


}
