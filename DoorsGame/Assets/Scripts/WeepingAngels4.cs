using UnityEngine;
using UnityEngine.AI; // Required for AI Navigation

public class WeepingAngels4 : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the Player GameObject
    [SerializeField] private float moveSpeed = 3f; // Normal speed of movement
    
    private NavMeshAgent agent;
    private bool isSeeking = false; // Flag to check if the AI should seek the player

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            Debug.LogWarning("Player not assigned to WeepingAngelsHoming script!");
        }

        agent.speed = moveSpeed; // Set the agent's speed
    }

    void Update()
    {
        // Continuously move the AI toward the player if seeking is enabled
        if (isSeeking && player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    // Method to start seeking the player
    public void ActivateSeek()
    {
        isSeeking = true;
    }

    // Optionally, you could add a method to stop seeking the player
    public void StopSeeking()
    {
        isSeeking = false;
        agent.ResetPath(); // Stops the AI from moving
    }

     private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player's Transform
        if (collision.transform == player)
        {
            Debug.Log("Weeping Angel destroyed!");
            Destroy(gameObject); // Destroy this Weeping Angel object
        }
    }
}
