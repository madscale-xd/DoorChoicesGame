using UnityEngine;
using TMPro; // For using TextMeshPro

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float startTime = 180f; // Start time in seconds (3 minutes = 180 seconds)
    [SerializeField] private TextMeshProUGUI timerText; // Reference to the TextMeshPro component to display time

    private float timeRemaining;
    private bool timerActive = false;

    [SerializeField] private SceneButtonManager scene;

    // 🎵 Background Music
    [SerializeField] private AudioSource audioSource;  // Assign an AudioSource in the Inspector
    [SerializeField] private AudioClip backgroundMusic; // Assign a music clip
    [SerializeField, Range(0f, 1f)] private float musicVolume = 1f; // Adjustable volume (0 - 1)

    void Start()
    {
        timeRemaining = startTime;
        audioSource.playOnAwake = false;  // Ensure it doesn’t auto-play
        audioSource.loop = true;  // Enable looping
        audioSource.volume = musicVolume; // Set volume before playing
        StartTimer();
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (timerActive)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                timerActive = false;
                TimerEnded();
            }

            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 🎵 Start the timer and play music
    public void StartTimer()
    {
        Debug.Log("🔄 StartTimer() called!");

        if (!timerActive)
        {
            timerActive = true;
            Debug.Log("✅ Timer started. Calling PlayMusic().");
            PlayMusic(); // Play music when the timer starts
        }
    }


    // ⏹ Stop the timer and stop music
    public void StopTimer()
    {
        timerActive = false;
    }

    // 🔄 Reset the timer and restart music
    public void ResetTimer()
    {
        timeRemaining = startTime;
        UpdateTimerDisplay();
        timerActive = false;
        StartTimer();
    }

    // ❌ When the timer ends, stop the music
    void TimerEnded()
    {
        Debug.Log("Timer has ended!");
        scene.PlayerDies();
    }

    // 🎵 Play background music (with debugging)
    private void PlayMusic()
    {
        Debug.Log("🔊 PlayMusic() called!");

        if (audioSource == null)
        {
            Debug.LogError("❌ AudioSource is missing! Assign it in the Inspector.");
            return;
        }

        if (backgroundMusic == null)
        {
            Debug.LogError("❌ Background music AudioClip is missing! Assign it in the Inspector.");
            return;
        }

        // 🔥 Force-stop & restart the audio to prevent "stuck" playback
        audioSource.Stop();
        audioSource.clip = backgroundMusic;
        audioSource.volume = musicVolume;
        audioSource.loop = true;
        audioSource.Play();

        Debug.Log("🎵 Now playing: " + backgroundMusic.name + " at volume: " + audioSource.volume);
    }



    // ⏹ Stop background music
    private void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("⏹ Stopped background music.");
        }
    }

    // 🔊 Adjust volume dynamically
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp(volume, 0f, 1f);
        if (audioSource != null)
        {
            audioSource.volume = musicVolume;
            Debug.Log("🔊 Music volume set to: " + musicVolume);
        }
    }
}
