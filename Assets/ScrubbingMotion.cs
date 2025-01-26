using UnityEngine;

public class ScrubbingMotion : MonoBehaviour
{
    public float moveSpeed = 2.0f;       // Speed at which the object moves left
    public float scrubDistance = 1.0f;  // Maximum distance the object scrubs back and forth
    public float scrubFrequency = 2.0f; // How often the scrubbing happens (in cycles per second)
    public float scrubChance = 0.3f;    // Probability (0 to 1) of scrubbing at each moment

    private bool isScrubbing = false;   // Whether the object is currently scrubbing
    private Vector3 originalPosition;   // To store the original position for scrubbing

    void Update()
    {
        if (isScrubbing)
        {
            PerformScrubMotion();
        }
        else
        {
            MoveLeft();

            // Randomly decide to start scrubbing
            if (Random.value < scrubChance * Time.deltaTime)
            {
                StartScrubbing();
            }
        }
    }

    /// <summary>
    /// Moves the object continuously to the left on the X-axis.
    /// </summary>
    private void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Starts the scrubbing motion by setting up necessary state.
    /// </summary>
    private void StartScrubbing()
    {
        isScrubbing = true;
        originalPosition = transform.position;
        StartCoroutine(ScrubCoroutine());
    }

    /// <summary>
    /// Executes the scrubbing motion.
    /// </summary>
    private void PerformScrubMotion()
    {
        // Oscillate back and forth on the X-axis based on time
        float scrubOffset = Mathf.Sin(Time.time * scrubFrequency * Mathf.PI * 2) * scrubDistance;
        transform.position = new Vector3(originalPosition.x + scrubOffset, transform.position.y, transform.position.z);
    }

    /// <summary>
    /// Ends the scrubbing motion after a short period of time.
    /// </summary>
    private System.Collections.IEnumerator ScrubCoroutine()
    {
        yield return new WaitForSeconds(1.0f); // Scrubbing duration (adjustable)
        isScrubbing = false;
    }
}
