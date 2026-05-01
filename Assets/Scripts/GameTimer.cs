using UnityEngine;
using TMPro;
using System;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerDisplay;
    [SerializeField] private float totalTimeMinutes = 12f;  // Total time in minutes
    
    private float timeRemaining;
    private bool isTimerRunning = false;
    
    // Events
    public event Action OnTimeUp;

    private void Start()
    {
        timeRemaining = totalTimeMinutes * 60f;  // Convert to seconds
        isTimerRunning = true;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (!isTimerRunning)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isTimerRunning = false;
            OnTimeUp?.Invoke();
            EndGame();
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        // Convert seconds back to minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        // Format as 12:00 style (MM:SS)
        if (timerDisplay != null)
        {
            timerDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            Debug.LogWarning("Timer Display not assigned in GameTimer!");
        }
    }

    private void EndGame()
    {
        Debug.Log("TIME'S UP! You lose!");
        // Add your game over logic here
        // For example: load a game over scene, show a game over panel, etc.
        
        // Example: Fade and load game over scene
        ScreenFader fader = FindObjectOfType<ScreenFader>();
        if (fader != null)
        {
            StartCoroutine(fader.FadeAndLoadScene("GameOver", 1f));
        }
        
        // Or disable gameplay
        Time.timeScale = 0f;  // Pause the game
    }

    public void PauseTimer()
    {
        isTimerRunning = false;
    }

    public void ResumeTimer()
    {
        isTimerRunning = true;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public void SetTotalTime(float minutes)
    {
        totalTimeMinutes = minutes;
        timeRemaining = totalTimeMinutes * 60f;
        UpdateTimerDisplay();
    }
}
