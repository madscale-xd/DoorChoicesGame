using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void QuitGame()
        {
        Application.Quit();
    }

    public void LoadGame()
    {
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

    public void PlayerDies(){
        StartCoroutine(LoseAfterDelay(1.5f)); // Starts a coroutine with a 2-second delay
    }

    IEnumerator LoseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Waits for 'delay' seconds
        LoadLose();
    }
}