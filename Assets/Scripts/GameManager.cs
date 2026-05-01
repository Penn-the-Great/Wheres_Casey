using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;
    public AudioClip ambientClip;  // Assign in Inspector

    private void Start()
    {
        // Initialize timer if not already assigned
        if (gameTimer == null)
        {
            gameTimer = FindObjectOfType<GameTimer>();
        }

        // Subscribe to timer events
        if (gameTimer != null)
        {
            gameTimer.OnTimeUp += HandleGameOver;
        }

        if (ambientClip != null)
        {
            Ambience.instance.PlayAmbientSound(ambientClip, 0.3f);
        }
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over: Time's up!");
        // Additional game over logic can be added here
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        if (gameTimer != null)
        {
            gameTimer.OnTimeUp -= HandleGameOver;
        }
    }
}
