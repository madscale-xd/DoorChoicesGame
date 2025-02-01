using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneButtonManager : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] private AudioSource musicSource;  // AudioSource for background music
    [SerializeField] private AudioClip backgroundMusic; // Background music clip
    [SerializeField, Range(0f, 1f)] private float musicVolume = 0.5f; // Volume control

    [Header("Sound Effects")]
    [SerializeField] private AudioSource sfxSource; // AudioSource for sound effects
    [SerializeField] private AudioClip loadGameSFX;  // SFX for LoadGame
    [SerializeField] private AudioClip quitGameSFX;  // SFX for QuitGame

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        PlayBackgroundMusic(); // Play background music on start
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void QuitGame()
    {
        PlaySFX(quitGameSFX); // Play SFX when quitting game
        Application.Quit();
    }

    public void LoadGame()
    {
        PlaySFX(loadGameSFX); // Play SFX when loading game
        SceneManager.LoadScene("DoorGame");
    }

    public void LoadLose()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    public void LoadWin()
    {
        SceneManager.LoadScene("WinScreen");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayerDies()
    {
        StartCoroutine(LoseAfterDelay(1.5f)); // Starts a coroutine with a 2-second delay
    }

    IEnumerator LoseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Waits for 'delay' seconds
        LoadLose();
    }

    // 🎵 Play Background Music when the scene starts
    private void PlayBackgroundMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.volume = musicVolume;
            musicSource.loop = true; // Loop the background music
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music is not set properly!");
        }
    }

    // 🎵 Play SFX
    private void PlaySFX(AudioClip sfx)
    {
        if (sfxSource != null && sfx != null)
        {
            sfxSource.PlayOneShot(sfx); // Play sound effect once
        }
        else
        {
            Debug.LogWarning("SFX Source or SFX Clip is missing!");
        }
    }
}
