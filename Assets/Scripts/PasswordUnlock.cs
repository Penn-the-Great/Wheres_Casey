using UnityEngine;
using TMPro;
using System;

public class PasswordUnlock : MonoBehaviour
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
            Debug.LogError("Password Input Field not assigned to PasswordUnlock!");
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
        OnUnlock();
        
        // Disable input after unlock
        if (passwordInput != null)
        {
            passwordInput.enabled = false;
        }
    }

    private void HandleIncorrectPassword()
    {
        Debug.Log("Incorrect password! Penalty applied!");
        
        ShowFeedback("Access Denied! +5 Minutes Penalty", incorrectColor);
        
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
        // This is where you can trigger door opening, UI changes, etc.
        // For example:
        // - Open a door GameObject
        // - Show a success animation
        // - Move to next area
        // - Unlock UI panel
        
        // Call an event or method in your door/lock manager
        GameObject lockedDoor = FindObjectOfType<Canvas>()?.transform.Find("LockedDoor")?.gameObject;
        if (lockedDoor != null)
        {
            lockedDoor.SetActive(false);  // Hide the locked door UI
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
