using UnityEngine;
using System.Collections;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float doorHp = 1f;

    [SerializeField] private AudioSource openAudioSource;  // Separate AudioSource for open sound
    [SerializeField] private AudioSource closeAudioSource; // Separate AudioSource for close sound
    [SerializeField] private AudioClip openSFX;            // Open door sound
    [SerializeField] private AudioClip closeSFX;           // Close door sound

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isClosing = false;

    private bool isSoundPlayed = false; // Track if sound has already been played

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(moveDistance, 0f, 0f);

        // Optional: Check if the audio sources are assigned
        if (openAudioSource == null || closeAudioSource == null)
        {
            Debug.LogWarning("Please assign both audio sources for open and close sounds.");
        }
    }

    void Update()
    {
        if (!openAudioSource.isPlaying && isSoundPlayed)
        {
            isSoundPlayed = false; // Reset flag to allow sound to play again
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }

        if (isClosing)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);

            if (transform.position == initialPosition)
            {
                isClosing = false;
                isMoving = false;
            }
        }

        MoveObject();
    }

    public void MoveObject()
    {
        if (doorHp <= 0 && !isMoving && !isSoundPlayed) // Only play sound once when door starts moving
        {
            isMoving = true;
            isSoundPlayed = true; // Set flag to prevent sound from playing again until reset
            Debug.Log("Playing Open Sound");
            PlaySound(openAudioSource, openSFX); // Play door opening sound
        }
    }

    public void ReduceHp(float amount)
    {
        doorHp -= amount;
        if (doorHp < 0) doorHp = 0;
    }

    public float GetHp()
    {
        return doorHp;
    }

    public void ResetPosition()
    {
        isMoving = false;
        moveSpeed *= 8.5f;
        targetPosition = initialPosition;
        isClosing = true;

        PlaySound(closeAudioSource, closeSFX); // Play door closing sound with separate AudioSource
        Debug.Log("Close door SFX should play.");

        StartCoroutine(DisableScriptAfterDelay(2f)); // Disable the script after 2 seconds
    }

    private void PlaySound(AudioSource audioSource, AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = false;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing.");
        }
    }

    private IEnumerator DisableScriptAfterDelay(float delay)
    {
        // Wait for the specified amount of time (in seconds)
        yield return new WaitForSeconds(delay);

        // Disable this script after the delay
        this.enabled = false;

        // Optionally, you can print a message to confirm the script was disabled
        Debug.Log("DoorHandler script disabled after delay.");
    }
}
