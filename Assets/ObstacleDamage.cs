using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision with " + other.gameObject.name);
        if(other.GetComponentInParent<BubbleMovement>() != null)
        {
            Debug.Log("dealt damage to " + other.gameObject.name);
            other.GetComponentInParent<BubbleMovement>().DealDamage(1);
        }
    }
}
