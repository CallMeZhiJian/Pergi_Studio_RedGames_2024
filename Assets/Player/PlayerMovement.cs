using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float laneDistance = 3.0f; // Distance between lanes
    public float moveSpeed = 10.0f;   // Speed of the player's lateral movement
    public float forwardSpeed = 5.0f;

    public TextMeshProUGUI scoreText; // Reference to the UI Text component for displaying the score
    private float score = 0.0f; // Player's score based on distance traveled

    private int desiredLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private Vector3 targetPosition;
    private Vector2 startTouchPosition;

    // Sound effects
    public AudioClip swipeSound;
    private AudioSource audioSource;

    void Start()
    {
        // Ensure score starts at 0
        UpdateScoreText();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Constant forward movement
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Update score based on distance traveled
        score += forwardSpeed * Time.deltaTime;
        UpdateScoreText();

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
        scoreText.text = Mathf.FloorToInt(score * 2).ToString();
    }
}
