using UnityEngine;
using UnityEngine.AI; // Required for AI Navigation

public class WeepingAngels : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the Player GameObject
    [SerializeField] private string triggerTag = "TriggerZone"; // Tag of the trigger object

    [SerializeField] private string visibilityTag = "Flashlight"; // Tag of the trigger object
    [SerializeField] private float moveSpeed = 3f; // Speed of movement

    private NavMeshAgent agent;
    private bool isChasing = false;
    private int activationZoneCount = 0; // Track how many overlapping triggers the enemy is inside

    private MeshRenderer angelMeshRenderer; // Specify MeshRenderer instead of Renderer

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        angelMeshRenderer = GetComponent<MeshRenderer>(); // Get the MeshRenderer component
        angelMeshRenderer.enabled = false; // Initially disable visibility

        if (player == null)
        {
            Debug.LogWarning("Player not assigned to EnemyAI script!");
        }

        agent.speed = moveSpeed;
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
        // Start chasing if the enemy collides with the trigger zone
        if (other.CompareTag(visibilityTag))
        {
            angelMeshRenderer.enabled = true; // Make the angel visible when in the activation zone
        }

        if (other.CompareTag(triggerTag))
        {
            isChasing = true;
            activationZoneCount++; // Increase count when entering a new activation zone
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(visibilityTag))
        {
            angelMeshRenderer.enabled = true; // Keep the angel visible when inside the activation zone
        }

        // Ensure it stays chasing as long as it's inside an activation zone
        if (other.CompareTag(triggerTag))
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(visibilityTag))
        {
            angelMeshRenderer.enabled = false; // Hide the angel when leaving the activation zone
        }

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
