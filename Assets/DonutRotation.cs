using UnityEngine;

public class DonutRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationDirection = new Vector3(30f, 60f, 90f); // Speed of rotation in degrees per second
    [SerializeField] private float rotationSpeed = 2f;

    private void Start()
    {
        transform.rotation = Random.rotation;
    }

    private void Update()
    {
        // Apply continuous rotation over time
        transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
