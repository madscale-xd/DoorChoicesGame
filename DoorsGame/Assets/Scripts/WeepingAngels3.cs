using UnityEngine;
using UnityEngine.AI; // Required for AI Navigation

public class WeepingAngels3 : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the Player GameObject
    [SerializeField] private string triggerTag = "TriggerZone"; // Tag of the activation zone
    [SerializeField] private string visibilityTag = "Flashlight"; // Tag of the visibility zone
    [SerializeField] private float moveSpeed = 3f; // Speed of movement

    private NavMeshAgent agent;
    private int activationZoneCount = 0; // Track number of overlapping activation zones
    private int visibilityZoneCount = 0; // Track number of overlapping visibility zones

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            Debug.LogWarning("Player not assigned to EnemyAI script!");
        }

        agent.speed = moveSpeed;
    }

    void Update()
    {
        // Enemy moves towards player only if inside a TriggerZone and NOT inside a Flashlight zone
        if (activationZoneCount > 0 && visibilityZoneCount == 0 && player != null)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath(); // Stop moving when conditions aren't met
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(visibilityTag))
        {
            visibilityZoneCount++;
        }

        if (other.CompareTag(triggerTag))
        {
            activationZoneCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(visibilityTag))
        {
            visibilityZoneCount = Mathf.Max(0, --visibilityZoneCount); // Prevent negative values
        }

        if (other.CompareTag(triggerTag))
        {
            activationZoneCount = Mathf.Max(0, --activationZoneCount); // Prevent negative values
        }
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
