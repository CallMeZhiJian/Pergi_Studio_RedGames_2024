using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float laneDistance = 3.0f; 
    public float moveSpeed = 10.0f;   
    public float forwardSpeed = 5.0f;

    public TextMeshProUGUI scoreText; 
    private float score = 0.0f; 
    private int currentScore ;
    private int highScore ;

    private int desiredLane = 1; 
    private Vector3 targetPosition;
    private Vector2 startTouchPosition;

    private Vector2 startMousePosition;
    private bool isSwiping = false;
    public bool canMove;

    // Sound effects
    public AudioClip swipeSound;
    private AudioSource audioSource;

    public GameObject hud;
    public GameObject gameOverUI;
    public GameObject fade;

    void Start()
    {
        UpdateScoreText();
        CheckForHighScore();
        audioSource = GetComponent<AudioSource>();
        canMove = false;

        gameOverUI.SetActive(false);
        hud.SetActive(true);
        StartCoroutine(StartingScene());
    }

    void Update()
    {
        if(canMove)
        {
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
            score += forwardSpeed * 2 * Time.deltaTime;
            UpdateScoreText();
        }

        // Detect swipe input
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeDelta = touch.position - startTouchPosition;

                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x > 0)
                    {
                        MoveRight();
                    }
                    else
                    {
                        MoveLeft();
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            Vector2 swipeDelta = (Vector2)Input.mousePosition - startMousePosition;

            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }
            }
            isSwiping = false;
        }

        // Calculate target position
        targetPosition = transform.position.z * Vector3.forward;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        // Move player smoothly to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    public void MoveLeft()
    {
        desiredLane--;
        if (desiredLane < 0)
        {
            desiredLane = 0;
        }
        audioSource.PlayOneShot(swipeSound);
    }

    public void MoveRight()
    {
        desiredLane++;
        if (desiredLane > 2)
        {
            desiredLane = 2;
        }
        audioSource.PlayOneShot(swipeSound);
    }

    void UpdateScoreText()
    {
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public float GetScore()
    {
        return score;
    }

    // Method to get the high score from PlayerPrefs
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    // Method to set a new high score in PlayerPrefs
    public void SetHighScore(int newHighScore)
    {
        PlayerPrefs.SetInt("HighScore", newHighScore);
    }

    // Call this method when the game is over to update the high score if necessary
    public void CheckForHighScore()
    {
        currentScore = Mathf.FloorToInt(score);
        highScore = GetHighScore();

        if (currentScore > highScore)
        {
            SetHighScore(currentScore);
            //highScoreText.text = currentScore.ToString();
        }
        else
        {
            //highScoreText.text = highScore.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Props"))
        {
            Debug.Log("Trigger");
            GameManager.instance.GetPropsDetails(other.GetComponent<Details>());

            Destroy(other);
        }
    }

    IEnumerator StartingScene()
    {
        hud.SetActive(false);
        yield return new WaitForSeconds(7);
        fade.SetActive(true);
        yield return new WaitForSeconds(3);
        hud.SetActive(true);
        fade.SetActive(false);
        canMove = true;
        yield return null;
    }
}
