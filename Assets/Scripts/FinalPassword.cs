using UnityEngine;
using TMPro;
using System;

public class FinalPassword : MonoBehaviour
{
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private string correctPassword = "1234";  // Set your password here
    [SerializeField] private int incorrectAttemptPenaltyMinutes = 5;  // Minutes added to timer on wrong password
    [SerializeField] private TextMeshProUGUI feedbackText;  // Text to show success/failure messages
    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color incorrectColor = Color.red;
    [SerializeField] private float feedbackDuration = 2f;  // How long to show feedback
    [SerializeField] private string gameOverSceneName = "YouDied";  // Name of your "You Died" scene
    [SerializeField] private float fadeOutDuration = 1f;
    
    private GameTimer gameTimer;
    private float feedbackTimer = 0f;
    private bool isUnlocked = false;

    private void Start()
    {
        // Find the GameTimer in the scene
        gameTimer = FindObjectOfType<GameTimer>();
        
        if (passwordInput != null)
        {
            // Subscribe to input field events
            passwordInput.onSubmit.AddListener(OnPasswordSubmitted);
        }
        else
        {
        
        }

        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }

    private void Update()
    {
        // Fade out feedback text
        if (feedbackTimer > 0)
        {
            feedbackTimer -= Time.deltaTime;
            if (feedbackTimer <= 0)
            {
                if (feedbackText != null)
                {
                    feedbackText.text = "";
                }
            }
        }
    }

    private void OnPasswordSubmitted(string password)
    {
        if (isUnlocked)
            return;

        if (password == correctPassword)
        {
            HandleCorrectPassword();
        }
        else
        {
            HandleIncorrectPassword();
        }

        // Clear the input field
        if (passwordInput != null)
        {
            passwordInput.text = "";
            passwordInput.ActivateInputField();
        }
    }

    private void HandleCorrectPassword()
    {
          // Pause the game first
        OnUnlock();
    }

    private void HandleIncorrectPassword()
    {
        Debug.Log("Incorrect password! Penalty applied!");
        
        ShowFeedback("Access Denied", incorrectColor);
        
        // Apply penalty to timer
        if (gameTimer != null)
        {
            gameTimer.AddGameMinutes(incorrectAttemptPenaltyMinutes);
        }
        else
        {
            Debug.LogWarning("GameTimer not found! Cannot apply penalty.");
        }
    }

    private void ShowFeedback(string message, Color color)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.color = color;
            feedbackTimer = feedbackDuration;
        }
    }

    private void OnUnlock()
    {
        // Pause the game first
        Time.timeScale = 0f;
        
        // Get the ScreenFader and fade to the "You Died" scene
        ScreenFader fader = FindObjectOfType<ScreenFader>();
        if (fader != null)
        {
            StartCoroutine(fader.FadeAndLoadScene(gameOverSceneName, fadeOutDuration, true));
        }
        else
        {
            Debug.LogError("ScreenFader not found in scene! Loading scene directly.");
            Time.timeScale = 1f;  // Reset timeScale
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameOverSceneName);
        }
        
    }

    public void SetCorrectPassword(string newPassword)
    {
        correctPassword = newPassword;
    }

    public void SetPenaltyMinutes(int minutes)
    {
        incorrectAttemptPenaltyMinutes = minutes;
    }

    public bool IsUnlocked()
    {
        return isUnlocked;
    }
}
