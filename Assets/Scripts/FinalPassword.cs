using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class FinalPassword : MonoBehaviour
{
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private string correctPassword = "1234";  // Set your password here
    [SerializeField] private int incorrectAttemptPenaltyMinutes = 5;  // Minutes added to timer on wrong password
    [SerializeField] private TextMeshProUGUI feedbackText;  // Text to show success/failure messages
    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color incorrectColor = Color.red;
    [SerializeField] private float feedbackDuration = 2f;  // How long to show feedback
    
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
        isUnlocked = true;
        Debug.Log("Correct password! Access granted!");
        
        ShowFeedback("Access Granted!", correctColor);
        
        // Unlock the door or trigger whatever event you need
        LoadByIndex();
        
        // Disable input after unlock
        if (passwordInput != null)
        {
            passwordInput.enabled = false;
        }
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

       public void LoadByIndex(int = 5)
    {
        SceneManager.LoadScene(5);
    
      Debug.Log("Correct password! Access granted!");
        
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
