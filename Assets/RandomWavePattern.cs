using UnityEngine;

public class RandomWavePattern : MonoBehaviour
{
    [SerializeField] private float amplitude = 2f;   // Maximum height of the wave
    [SerializeField] private float frequency = 1f;   // Speed of the wave motion
    [SerializeField] private float horizontalMovementSpeed = 5f;
    private float currentSpeed;
    [SerializeField] private float randomOffset = 0.5f;
    [SerializeField] private float randomHeightOffset = 1f;

    private Vector3 startPosition;
    private Vector3 instantiatePosition;


    private void Awake()
    {
        instantiatePosition = transform.position;

        transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-randomHeightOffset, randomHeightOffset), transform.position.z);

        // Save the initial position to use as the base for the up-and-down motion
        startPosition = transform.position;

        currentSpeed = Random.Range(horizontalMovementSpeed, horizontalMovementSpeed + randomOffset * 3);
    }

    void Start()
    {


    }

    private void OnEnable()
    {
        ResetDuck();
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave
        float wave = Mathf.Sin(Time.time * frequency) * amplitude;

        // Set the new position (up and down only)
        transform.position = new Vector3(transform.position.x - currentSpeed * Time.deltaTime, startPosition.y + wave, startPosition.z);
    }

    private void ResetDuck()
    {
        transform.position = instantiatePosition;

        transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-randomHeightOffset, randomHeightOffset), transform.position.z);

        // Save the initial position to use as the base for the up-and-down motion
        startPosition = transform.position;


        currentSpeed = Random.Range(horizontalMovementSpeed, horizontalMovementSpeed + randomOffset * 3);
    }
}
