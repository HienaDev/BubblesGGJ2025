using UnityEngine;

public class DogBiteMotion : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 2f;      // Height of the jump
    [SerializeField] private float jumpDuration = 1f;    // Time it takes to complete one jump
    [SerializeField] private float xRange = 5f;         // Range for random X reappearance
    [SerializeField] private int repetitions = 3;       // Number of times the motion repeats
    private int currentRepetitions;
    [SerializeField] private float minCooldown = 0.5f;  // Minimum cooldown time
    [SerializeField] private float maxCooldown = 2f;    // Maximum cooldown time

    private Vector3 startPosition;    // Starting position of the object
    private Vector3 instantiatedPosition;
    private float elapsedTime;        // Tracks time for the up-and-down motion
    private int currentRepetition;    // Tracks the number of completed repetitions
    private float cooldownTime;       // Time remaining before the next bite motion
    private bool isCooldownActive;    // Indicates whether cooldown is active


    private void Awake()
    {
        instantiatedPosition = transform.position;
        // Save the starting position and initialize variables
        startPosition = transform.position;
        elapsedTime = 0f;
        currentRepetition = 0;
        isCooldownActive = false;
        cooldownTime = Random.Range(minCooldown, maxCooldown); // Set a random cooldown initially

        currentRepetitions = Random.Range(repetitions - 1, repetitions + 1);
    }


    private void OnEnable()
    {
        ResetDog();
    }

    private void ResetDog()
    {
        transform.position = instantiatedPosition;
        startPosition = transform.position;
        elapsedTime = 0f;
        cooldownTime = Random.Range(minCooldown, maxCooldown); // Set a random cooldown initially
        currentRepetitions = Random.Range(repetitions - 1, repetitions + 1);
        isCooldownActive = false;
    }

    void Update()
    {
        if (currentRepetition < currentRepetitions)
        {
            if (!isCooldownActive)
            {
                // Calculate progress in the jump cycle (0 to 1)
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / jumpDuration;

                // Calculate vertical position using a parabola for the jump-like motion
                float yOffset = Mathf.Sin(Mathf.PI * progress) * jumpHeight;

                // Update the object's position
                transform.position = new Vector3(startPosition.x, startPosition.y + yOffset, startPosition.z);

                // Check if the jump is complete
                if (progress >= 1f)
                {
                    // Reset time and reappear at a random x-coordinate
                    elapsedTime = 0f;
                    startPosition.x = Random.Range(-xRange, xRange);
                    transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);

                    // Increment the repetition count and activate cooldown
                    currentRepetition++;
                    isCooldownActive = true;
                }
            }
            else
            {
                // Countdown the cooldown time
                cooldownTime -= Time.deltaTime;

                // If the cooldown is complete, start the next bite
                if (cooldownTime <= 0f)
                {
                    // Reset cooldown and start the next jump
                    isCooldownActive = false;
                    cooldownTime = Random.Range(minCooldown, maxCooldown); // Set a new random cooldown
                }
            }
        }
        else
        {
            // Stop the motion after completing all repetitions
            gameObject.SetActive(false);
        }
    }
}
