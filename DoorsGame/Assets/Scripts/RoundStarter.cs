using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStarter : MonoBehaviour
{
    [SerializeField] private CountdownTimer countdownTimer; // Serialized reference to the CountdownTimer script
    [SerializeField] private DoorHandler door;

    // This method is called when a trigger event occurs (with the player)
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with has the tag "Player"
        if (other.CompareTag("Playerr"))
        {
            // Call the ResetTimer method on the CountdownTimer
            countdownTimer.ResetTimer();

            door.ResetPosition();

            // Destroy this object after calling ResetTimer
            Destroy(gameObject);
        }
    }
}
