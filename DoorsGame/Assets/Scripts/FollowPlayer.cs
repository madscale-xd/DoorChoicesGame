using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the Player GameObject

    void Update()
    {
        if (player != null)
        {
            // Update this object's position to match the player's position
            transform.position = player.position;
        }
    }
}
