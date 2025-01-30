using UnityEngine;

public class DoorDamager : MonoBehaviour
{
    [SerializeField] private DoorHandler door; // Assign the DoorHandler in the Inspector
    [SerializeField] private Transform player; // Assign the Player in the Inspector
    [SerializeField] private float damageAmount = 1f; // Amount to subtract from doorHp
    private bool hasCollided = false; // Ensure it only happens once

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == player && !hasCollided)
        {
            hasCollided = true; // Prevent multiple collisions
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
}
