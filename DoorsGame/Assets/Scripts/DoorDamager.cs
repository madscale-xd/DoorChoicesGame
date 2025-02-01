using UnityEngine;

public class DoorDamager : MonoBehaviour
{
    [SerializeField] private DoorHandler door; // Assign the DoorHandler in the Inspector
    [SerializeField] private Transform player; // Assign the Player in the Inspector
    [SerializeField] private float damageAmount = 1f; // Amount to subtract from doorHp
    private bool hasCollided = false; // Ensure it only happens once

    // 🎵 Sound Effect
    [SerializeField] private AudioSource audioSource; // Assign in Inspector
    [SerializeField] private AudioClip collisionSFX; // Assign collision sound

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == player && !hasCollided)
        {
            hasCollided = true; // Prevent multiple collisions
            PlayCollisionSound();
            SubtractDoorHp();
            Destroy(gameObject); // Destroy this object after collision
        }
    }

    private void SubtractDoorHp()
    {
        if (door != null)
        {
            door.ReduceHp(damageAmount);
            Debug.Log("Door HP reduced! Remaining HP: " + door.GetHp());
        }
    }

    private void PlayCollisionSound()
    {
        if (audioSource != null && collisionSFX != null)
        {
            audioSource.PlayOneShot(collisionSFX); // Play sound effect once
        }
        else
        {
            Debug.LogWarning("⚠️ Missing AudioSource or Collision SFX in DoorDamager!");
        }
    }
}
