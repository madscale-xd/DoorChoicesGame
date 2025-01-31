using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float doorHp = 1f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(moveDistance, 0f, 0f);
    }

    void Update()
    {
        // Move the object if it's set to move
        MoveObject();

        if (isMoving)
        {
            // Move towards the target position smoothly
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Stop the movement once it reaches the target
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    public void MoveObject()
    {
        // If door HP is reduced, move the door
        if (doorHp <= 0)
        {
            isMoving = true;
        }
    }

    public void ReduceHp(float amount)
    {
        doorHp -= amount;
        if (doorHp < 0) doorHp = 0;
    }

    public float GetHp()
    {
        return doorHp;
    }

    // Method to reset the door to its initial position
    public void ResetPosition()
    {
        moveSpeed*=8.5f;
        targetPosition = initialPosition;  // Set the target back to the original position
        isMoving = true;  // Start moving the door back
    }
}
