using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of the player movement
    [SerializeField] private float rotationSpeed = 10f; // Speed of player rotation
    [SerializeField] private Camera mainCamera; // Assign the main camera in the Inspector
    [SerializeField] private TextMeshProUGUI hpText; // Assign the TextMeshPro component in the Inspector

    private int playerHp = 3;

    private Rigidbody rb;
    private Vector3 moveDirection;

    private string[] romanNumerals = { "0", "I", "II" }; // Roman numeral representation of HP


    [SerializeField] private SceneButtonManager scene;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent unwanted rotation

        // Auto-assign camera if not set
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Get input from WASD keys
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S or Up/Down

        // Convert input into movement direction
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Smoothly rotate player to face the mouse cursor
        RotateTowardsMouse();
    }

    void FixedUpdate()
    {
        // Apply movement with physics-based approach
        rb.velocity = moveDirection * moveSpeed;
    }

    void RotateTowardsMouse()
    {
        if (mainCamera == null) return;

        // Create a ray from the camera to the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // If the ray hits something (assumed to be the ground)
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Keep rotation only on the Y-axis

            // Get direction to look at
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Get target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            playerHp--;
            UpdateHpText();
            if (playerHp == 0)
            {
                scene.PlayerDies();
                gameObject.SetActive(false);
            }
        }
    }

    private void UpdateHpText()
    {
        if (hpText != null)
        {
            hpText.text = romanNumerals[Mathf.Max(playerHp, 0)]; // Prevents index errors
        }
    }
}
