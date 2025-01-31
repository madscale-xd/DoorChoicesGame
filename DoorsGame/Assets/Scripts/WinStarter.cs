using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStarter : MonoBehaviour
{
    [SerializeField] SceneButtonManager scene;
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with has the tag "Player"
        if (other.CompareTag("Playerr"))
        {
            scene.LoadWin();
        }
    }
}
