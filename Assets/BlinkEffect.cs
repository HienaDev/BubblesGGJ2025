using UnityEngine;
using System.Collections;
public class BlinkEffect : MonoBehaviour
{
    public float blinkDuration = 1.0f; // Total duration of the blinking effect
    public int blinkCount = 3;        // Number of blinks

    private Material objectMaterial; // Internal reference to the material

    void Start()
    {
        // Ensure the object has a Renderer and create a unique material instance
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            objectMaterial = renderer.material; // Assign a unique material instance
        }
        else
        {
            Debug.LogError("No Renderer found on the object!");
        }

    }

    /// <summary>
    /// Public method to start the blinking effect.
    /// </summary>
    public void StartBlink()
    {
        if (objectMaterial != null)
        {
            // Start the blink effect
            StartCoroutine(BlinkRoutine());
        }
        else
        {
            Debug.LogError("No material assigned or found on the object.");
        }
    }

    /// <summary>
    /// Coroutine to handle the blinking effect.
    /// </summary>
    private IEnumerator BlinkRoutine()
    {
        // Get the original color of the material
        Color baseColor = objectMaterial.color;

        // Calculate the time for each blink phase
        float blinkPhaseDuration = blinkDuration / (blinkCount * 2);

        for (int i = 0; i < blinkCount; i++)
        {
            // Fade in (to full alpha)
            yield return StartCoroutine(FadeAlpha(baseColor, 0f, 1f, blinkPhaseDuration));

            // Fade out (to 0 alpha)
            yield return StartCoroutine(FadeAlpha(baseColor, 1f, 0f, blinkPhaseDuration));
        }

        // Ensure the material ends with 0 alpha
        objectMaterial.color = new Color(baseColor.r, baseColor.g, baseColor.b, 0f);
    }

    /// <summary>
    /// Coroutine to smoothly fade the material's alpha between two values over a given duration.
    /// </summary>
    private IEnumerator FadeAlpha(Color baseColor, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);

            // Update the material color with the new alpha
            objectMaterial.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);

            yield return null;
        }

        // Set the final alpha value for precision
        objectMaterial.color = new Color(baseColor.r, baseColor.g, baseColor.b, endAlpha);
    }
}
