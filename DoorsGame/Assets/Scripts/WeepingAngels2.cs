using UnityEngine;
using UnityEngine.AI; // Required for AI Navigation

public class WeepingAngels2 : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the Player GameObject
    [SerializeField] private string triggerTag = "ActivationZone"; // Tag of the trigger object
    [SerializeField] private float moveSpeed = 3f; // Speed of movement

    private NavMeshAgent agent;
    private bool isChasing = false;
    private int activationZoneCount = 0; // Track how many overlapping triggers the enemy is inside

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component

        if (player == null)
        {
            Debug.LogWarning("Player not assigned to EnemyAI script!");
        }

        agent.speed = moveSpeed; // Set movement speed
    }

    void Update()
    {
        // If chasing, update destination
        if (isChasing && player != null)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath(); // Stop moving when not chasing
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(triggerTag))
        {
            isChasing = true;
            activationZoneCount++; // Increase count when entering a new activation zone
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Ensure it stays chasing as long as it's inside an activation zone
        if (other.CompareTag(triggerTag))
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reduce count when leaving an activation zone
        if (other.CompareTag(triggerTag))
        {
            activationZoneCount--;

            // If no activation zones remain, stop chasing
            if (activationZoneCount <= 0)
            {
                isChasing = false;
                activationZoneCount = 0; // Prevent negative counts
            }
        }
    }
}
