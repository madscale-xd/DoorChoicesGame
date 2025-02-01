using UnityEngine;
using System.Collections; // Required for Coroutines

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float doorHp = 1f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSFX;
    [SerializeField] private AudioClip closeSFX;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isClosing = false;

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

            // Stop moving when the door reaches its target
            if (transform.position == targetPosition)
            {
                isMoving = false;
                
            }
        }

        if (isClosing)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);

            if (transform.position == initialPosition)
            {
                isClosing = false;
                
            }
        }
    }

    public void MoveObject()
    {
        if (doorHp <= 0 && !isMoving)
        {
            isMoving = true;
            PlaySound(openSFX); // Play door opening sound
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

    public void ResetPosition()
    {
        moveSpeed *= 8.5f;
        targetPosition = initialPosition;
        isClosing = true;
        //PlaySound(closeSFX); // Play door closing sound
        Debug.Log("close door sfx should play");
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = false;
            audioSource.Play();
        }
    }
}

