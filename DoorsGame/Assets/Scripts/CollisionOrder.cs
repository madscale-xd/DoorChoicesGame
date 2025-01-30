using UnityEngine;

public class CollisionOrder : MonoBehaviour
{
    private string[] requiredTags = { "Fish", "Wine", "Cross" }; // Define the required order of tags
    private int currentTagIndex = 0; // Track which tag in the sequence we are expecting

    // Reference to the WeepingAngels4 script (can be set in the inspector or found in the scene)
    private WeepingAngels4[] weepingAngels;

    void Start()
    {
        // Find all objects in the scene with the WeepingAngels4 script attached
        weepingAngels = FindObjectsOfType<WeepingAngels4>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Only check when colliding with an object named "ThirdKey"
        if (collision.gameObject.name == "ThirdKey")
        {
            // Check if the collided object has the correct tag in the correct order
            if (collision.gameObject.CompareTag(requiredTags[currentTagIndex]))
            {
                // If the player collides with the correct tag, move to the next tag
                currentTagIndex++;
                Debug.Log("Correct");

                // If the player has completed the sequence, do something good (optional)
                if (currentTagIndex == requiredTags.Length)
                {
                    Debug.Log("Correct sequence completed!");
                    // You can put some positive action here, like giving a reward or progressing in the game
                }
            }
            else
            {
                // If the player collides with the wrong tag, do something bad
                Debug.Log("Wrong sequence! Something bad happens.");

                // Call ActivateSeek on all WeepingAngels4 objects
                foreach (WeepingAngels4 angel in weepingAngels)
                {
                    angel.ActivateSeek(); // Activates the seeking behavior
                }

                // You can implement additional bad consequences here
            }
        }
    }
}
