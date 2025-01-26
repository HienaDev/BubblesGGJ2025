using UnityEngine;

public class RainbowArcMovement : MonoBehaviour
{
    [SerializeField] private float arcHeight = 5f;      // Maximum height of the arc
    [SerializeField] private float heightOffset = 0.5f;
    private float currentHeight;
    [SerializeField] private float arcLength = 10f;     // Total length of the arc
    [SerializeField] private float lengthOffset = 0.5f;
    private float currentLength;
    [SerializeField] private float speed = 2f;         // Speed of the movement
    [SerializeField] private float speedOffset = 0.5f;
    private float currentSpeed;

    [SerializeField] private float randomStartOffset = 2f;

    private Vector3 startPosition;
    private Vector3 instantiatePosition;
    private float progress = 0f; // Tracks the progress of the arc (0 to 1)


    private void Awake()
    {
        instantiatePosition = transform.position;
        // Save the starting position
        startPosition = transform.position;
        transform.position = new Vector3(transform.position.x + Random.Range(0, randomStartOffset), transform.position.y, transform.position.z);


        currentHeight = Random.Range(arcHeight - heightOffset, arcHeight + heightOffset);

        currentLength = Random.Range(arcLength - lengthOffset, arcLength + lengthOffset);

        currentSpeed = Random.Range(speed - speedOffset, speedOffset + speedOffset);
    }

    void Start()
    {

    }

    private void OnEnable()
    {
        ResetBaby();
    }

    private void ResetBaby()
    {
        transform.position = instantiatePosition;

        startPosition = transform.position;
        transform.position = new Vector3(transform.position.x + Random.Range(0, randomStartOffset), transform.position.y, transform.position.z);


        currentHeight = Random.Range(arcHeight - heightOffset, arcHeight + heightOffset);

        currentLength = Random.Range(arcLength - lengthOffset, arcLength + lengthOffset);

        currentSpeed = Random.Range(speed - speedOffset, speedOffset + speedOffset);
    }

    void Update()
    {
        // Update progress based on speed and time
        progress += currentSpeed * Time.deltaTime;

        // Calculate the horizontal position
        float x = progress * currentLength;

        // Calculate the vertical position using a parabolic equation
        float y = Mathf.Sin(Mathf.PI * progress) * currentHeight;

        // Move the object along the arc
        transform.position = startPosition + new Vector3(x, y, 0);

        // Reset progress when the arc is completed
        if (progress >= 1f)
        {
            progress = 0f; // Restart the arc motion
            gameObject.SetActive(false);
        }
    }
}
