using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject gameOverPanel; 
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI highScoreText; 
    public TextMeshProUGUI coinText; 
    public TextMeshProUGUI coinHUDText; 
    public GameObject hud; 
    public GameObject revivePanel;
    private PlayerMovement playerMovement;
    public int reviveCost = 5;
    public bool revived;

    public Animator gameOverPanelAnimation;
    private bool isGameOverPanelOpen = false;

    public Animator transitionAnimation;
    private bool isPauseOpen = false;

    void Start()
    {
        coinHUDText.text= PlayerPrefs.GetInt("Coins").ToString();
        revived = false;   
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

    public void ReviveRequest()
    {
        playerMovement.canMove = false;
        playerMovement.canPlay = false;
        if (PlayerPrefs.GetInt("Coins") >= reviveCost)
        {
            Debug.Log("RequestRevive");
            hud.SetActive(false);
            revivePanel.SetActive(true);
        }
        else 
        { 
            GameOver(); 
        }
    }

    public void Revive()
    {
        if (PlayerPrefs.GetInt("Coins") >= reviveCost)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") -5);
            coinText.text = PlayerPrefs.GetInt("Coins").ToString();
            coinHUDText.text = PlayerPrefs.GetInt("Coins").ToString();

            playerMovement.canMove = true;
            playerMovement.canPlay = true;
            hud.SetActive(true);
            revivePanel.SetActive(false);

            revived = true;

            GameManager.instance.ReviveClearTiles();
        }
        else
        {
            GameOver();
            Debug.Log("GameOver");
        }
    }

    public void GameOver()
    {
        hud.SetActive(false);
        revivePanel.SetActive(false);
        // Display the Game Over panel
        gameOverPanel.SetActive(true);

        isGameOverPanelOpen = !isGameOverPanelOpen;
        gameOverPanelAnimation.SetBool("OpenGameOverPanel", isGameOverPanelOpen);

        if (playerMovement != null)
        {
            scoreText.text = Mathf.FloorToInt(playerMovement.GetScore()).ToString();
            playerMovement.CheckForHighScore();
            highScoreText.text = Mathf.FloorToInt(playerMovement.GetHighScore()).ToString();
            coinText.text = PlayerPrefs.GetInt("Coins").ToString();
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

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

    public void BacktoMenu()
    {
        Audio_MainMenu.instance.PlaySFX(0);
        Audio_MainMenu.instance.bgmAudioSource1.Stop();
        Audio_MainMenu.instance.bgmAudioSource2.Stop();
        Time.timeScale = 1f;
        isPauseOpen = !isPauseOpen;
        transitionAnimation.SetBool("Transition", isPauseOpen);
        SceneManager.LoadScene("MainMenu");
    }


}
