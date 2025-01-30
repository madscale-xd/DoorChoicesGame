using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float doorHp = 1;

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
        MoveObject();
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void MoveObject()
    {
        if (doorHp <= 0) // Now works correctly with reduced HP
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
}
