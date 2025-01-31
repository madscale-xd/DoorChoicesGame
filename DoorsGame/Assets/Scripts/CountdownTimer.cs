using UnityEngine;
using TMPro; // For using TextMeshPro

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float startTime = 180f; // Start time in seconds (3 minutes = 180 seconds)
    [SerializeField] private TextMeshProUGUI timerText; // Reference to the TextMeshPro component to display time

    private float timeRemaining;
    private bool timerActive = false;

    [SerializeField] SceneButtonManager scene;

    void Start()
    {
        timeRemaining = startTime; // Set time remaining to start time
        StartTimer();
        UpdateTimerDisplay();
    }

    void Update()
    {
        // Start countdown when timer is active
        if (timerActive)
        {
            timeRemaining -= Time.deltaTime; // Decrease time by the time elapsed in the frame

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f; // Prevent going below 0
                timerActive = false; // Stop the timer when it hits 0
                TimerEnded();
            }

            UpdateTimerDisplay(); // Update the display each frame
        }
    }

    void UpdateTimerDisplay()
    {
        // Convert timeRemaining into minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Display the time in Minute:Seconds format
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Method to start the timer (can be called from another script or button)
    public void StartTimer()
    {
        timerActive = true;
    }

    // Method to stop the timer
    public void StopTimer()
    {
        timerActive = false;
    }

    // Method to reset the timer back to 3:00 (180 seconds)
    public void ResetTimer()
    {
        timeRemaining = startTime; // Reset the timer to the original start time
        UpdateTimerDisplay(); // Update the display to reflect the reset
        timerActive = false; // Optionally, stop the timer when reset
        StartTimer();
    }

    // Method to handle what happens when the timer ends (optional)
    void TimerEnded()
    {
        Debug.Log("Timer has ended!");
        // Handle any logic when the timer reaches 0 (e.g., triggering a game over, etc.)
        scene.PlayerDies();
    }
}
