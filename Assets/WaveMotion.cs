using UnityEngine;

public class WaveMotion : MonoBehaviour
{
    // Variables to control the motion
    public float speed = 2.0f;     // Speed of the wave motion
    public float amplitude = 2.0f; // Amplitude of the wave
    public float frequency = 1.0f; // Frequency of the wave

    private Vector3 startPosition;

    void Start()
    {
        // Record the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate time-based offsets
        float time = Time.time * speed;

        // Wave-like motion in X and Y
        float x = Mathf.Cos(time * frequency) * amplitude;
        float y = Mathf.Sin(time * frequency) * amplitude;

        // Update the object's position
        transform.position = startPosition + new Vector3(x, y, 0f);
    }
}
