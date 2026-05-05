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
    [SerializeField] private float realSecondsPerGameMinute = 0.2f;  // 2 real minutes = 10 game minutes (120s / 10 = 12s per game minute, or 0.2 game minutes per real second)
    [SerializeField] private string gameOverSceneName = "YouDied";  // Name of your "You Died" scene
    [SerializeField] private float fadeOutDuration = 1f;  // How long the fade takes
    
    private int currentHour;
    private int currentMinute;
    private bool isTimerRunning = false;
    private float timeAccumulator = 0f;
    private bool gameOverTriggered = false;  // Prevent multiple calls
    
    // Events
    public event Action OnTimeUp;

    private void Start()
    {
        // Initialize to start time (6 PM)
        currentHour = startHour;
        currentMinute = startMinute;
        isTimerRunning = true;
        gameOverTriggered = false;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (!isTimerRunning)
            return;

        timeAccumulator += Time.deltaTime;

        // Calculate game minutes to add based on real time
        // If 10 game minutes = 120 real seconds, then 1 real second = 10/120 = 0.0833 game minutes
        float gameMinutesToAdd = timeAccumulator / (60f / (10f / (realSecondsPerGameMinute * 60f)));

        if (gameMinutesToAdd >= 1f)
        {
            AddGameMinutes((int)gameMinutesToAdd);
            timeAccumulator = 0f;
        }

        UpdateTimerDisplay();

        // Check if we've reached midnight
        if (currentHour == endHour && currentMinute == endMinute && !gameOverTriggered)
        {
            gameOverTriggered = true;
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

    public void SetGameSpeed(float realSecondsPerGameMinute)
    {
        // Adjust how fast game time passes
        // e.g., 0.2 = 10 game minutes per 2 real minutes
        // e.g., 0.1 = 10 game minutes per 1 real minute (faster)
        // e.g., 0.4 = 10 game minutes per 4 real minutes (slower)
        this.realSecondsPerGameMinute = realSecondsPerGameMinute;
    }
}
