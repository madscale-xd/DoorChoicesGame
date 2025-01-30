using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign the player GameObject in the Inspector

    [SerializeField] private float height = 10f; // Height of the camera above the player
    [SerializeField] private float followSpeed = 5f; // How smoothly the camera follows the player
    [SerializeField] private float viewAngle = 60f; // Camera's downward tilt angle
    [SerializeField] private float zOffset = -5f; // Z position offset for adjusting camera distance

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player object not assigned to CameraFollow script.");
            return;
        }

        // Desired position (above and slightly behind the player)
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + height, player.position.z + zOffset);

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Lock camera rotation to always look downward
        transform.rotation = Quaternion.Euler(viewAngle, 0f, 0f);
    }
}
