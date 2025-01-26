using UnityEngine;

public class SphereCollisionFollower : MonoBehaviour
{

    [SerializeField] private GameObject collisionBubble;
    public GameObject currentBubble;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnBubble();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBubble != null)
        {
            currentBubble.transform.position = transform.position;
        }
    }

    public void SpawnBubble()
    {
        GameObject bubbleSphere = GetComponentInChildren<TAG_BubbleSphere>().gameObject;

        if(currentBubble != null)
        {
            Destroy(currentBubble);
        }

        currentBubble = Instantiate(collisionBubble);
        currentBubble.transform.localScale = bubbleSphere.transform.localScale * 10;
    }
}
