using UnityEngine;
using TMPro;
using System;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerDisplay;
    [SerializeField] private int startHour = 18;  // 6 PM in 24-hour format
    [SerializeField] private int startMinute = 0;
    [SerializeField] private int endHour = 0;    // Midnight (00:00)
    [SerializeField] private int endMinute = 0;
    [SerializeField] private float minutesPerRealSecond = 10f;  // 10 game minutes per real second
    
    private int currentHour;
    private int currentMinute;
    private bool isTimerRunning = false;
    private float timeAccumulator = 0f;
    
    // Events
    public event Action OnTimeUp;

    private void Start()
    {
        // Initialize to start time (6 PM)
        currentHour = startHour;
        currentMinute = startMinute;
        isTimerRunning = true;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (!isTimerRunning)
            return;

        timeAccumulator += Time.deltaTime;

        // Add game minutes based on real time
        if (timeAccumulator >= 1f)
        {
            AddGameMinutes((int)(timeAccumulator * minutesPerRealSecond));
            timeAccumulator = 0f;
        }

        UpdateTimerDisplay();

        // Check if we've reached midnight
        if (currentHour == endHour && currentMinute == endMinute)
        {
            isTimerRunning = false;
            OnTimeUp?.Invoke();
            EndGame();
        }
    }

    private void AddGameMinutes(int minutes)
    {
        currentMinute += minutes;

        // Handle minute overflow
        while (currentMinute >= 60)
        {
            currentMinute -= 60;
            currentHour += 1;
        }

        // Handle hour overflow (after 23:59 comes 00:00)
        if (currentHour >= 24)
        {
            currentHour = 0;
        }
    }

    private void UpdateTimerDisplay()
    {
        // Format as 18:00 style (HH:MM) in 24-hour format
        if (timerDisplay != null)
        {
            timerDisplay.text = string.Format("{0:00}:{1:00}", currentHour, currentMinute);
        }
        else
        {
            Debug.LogWarning("Timer Display not assigned in GameTimer!");
        }
    }

    private void EndGame()
    {
        Debug.Log("TIME'S UP! You lose! It's now midnight!");
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

    public string GetCurrentTime()
    {
        return string.Format("{0:00}:{1:00}", currentHour, currentMinute);
    }

    public void SetStartTime(int hour, int minute)
    {
        startHour = hour;
        startMinute = minute;
        currentHour = hour;
        currentMinute = minute;
        UpdateTimerDisplay();
    }

    public void SetEndTime(int hour, int minute)
    {
        endHour = hour;
        endMinute = minute;
    }

    public void SetGameMinutesPerRealSecond(float rate)
    {
        minutesPerRealSecond = rate;
    }
}
