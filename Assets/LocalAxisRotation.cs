using UnityEngine;

public class LocalAxisRotation : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.right; // The axis of rotation in local space
    public float rotationSpeed = 90f;        // Rotation speed in degrees per second

    void Update()
    {
        // Calculate the rotation for this frame
        float rotationStep = rotationSpeed * Time.deltaTime;

        // Apply rotation around the local axis
        transform.Rotate(rotationAxis, rotationStep, Space.Self);
    }
}
