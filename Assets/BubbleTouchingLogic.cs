using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleTouchingLogic : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer; // Assign the specific layer in the Inspector

    private BubbleMovement bubbleScript;

    public GameObject monster;
    public List<GameObject> monstersPivotsInside;
    public List<GameObject> monstersInside;

    [SerializeField] private GameObject monsterPivotPrefab;

    private void Start()
    {
        bubbleScript = GetComponent<BubbleMovement>();

        monstersInside = new List<GameObject>();

        StartCoroutine(CreateNewMonster(monster));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collosion with " + gameObject.name);
        int x = 1 << collision.gameObject.layer;
        // Check if the collided object is in the target layer
        if (x == targetLayer.value)
        {

            if (bubbleScript.playersOnThisBubble.Count >
                collision.gameObject.GetComponent<BubbleMovement>().playersOnThisBubble.Count)
            {
                CreateNewBubble(bubbleScript, collision.gameObject);
            }
            else if (bubbleScript.bubbleHealth >
                collision.gameObject.GetComponent<BubbleMovement>().bubbleHealth)
            {
                CreateNewBubble(bubbleScript, collision.gameObject);
            }

            Debug.Log("Collided with object in the target layer: " + collision.gameObject.name);
        }
    }

    private void CreateNewBubble(BubbleMovement thisBubble, GameObject collidingBubble)
    {
        Debug.Log("test");

        BubbleMovement collidingBubbleScript = collidingBubble.GetComponent<BubbleMovement>();





        // Give control to the new player on that bubble
        foreach (int player in collidingBubbleScript.playersOnThisBubble)
        {
            thisBubble.GetComponent<BubbleMovement>().AddPlayer(player);
        }

        foreach (GameObject monsterInOtherBubble in collidingBubble.GetComponent<BubbleTouchingLogic>().monstersInside)
        {
            StartCoroutine((CreateNewMonster(monsterInOtherBubble)));
        }

        if (collidingBubbleScript.keyboard1)
        {
            gameObject.GetComponent<BubbleMovement>().keyboard1 = true;

        }

        if (collidingBubbleScript.keyboard2)
        {
            gameObject.GetComponent<BubbleMovement>().keyboard2 = true;

        }

        if (collidingBubbleScript.keyboard3)
        {
            gameObject.GetComponent<BubbleMovement>().keyboard3 = true;

        }

        if (collidingBubbleScript.controller1)
        {
            gameObject.GetComponent<BubbleMovement>().controller1 = true;

        }


        if (collidingBubbleScript.buzz1)
        {
            gameObject.GetComponent<BubbleMovement>().buzz1 = true;

        }

        if (collidingBubbleScript.buzz2)
        {
            gameObject.GetComponent<BubbleMovement>().buzz2 = true;

        }

        if (collidingBubbleScript.buzz3)
        {
            gameObject.GetComponent<BubbleMovement>().buzz3 = true;

        }

        if (collidingBubbleScript.buzz4)
        {
            gameObject.GetComponent<BubbleMovement>().buzz4 = true;

        }

        // Increase scale of the bigger bubble (with an update that keeps scaling until the target scale)
        // ALSO SCALE THE COLLISION WITH MONSTERS ONE AT THE SAME SPEED

        // Check which bubble has more players, if there's a draw, check which has more health, then descale the monster on the one with the least,
        // and spawn it and scale it on the one with the most

        Destroy(collidingBubbleScript.gameObject.GetComponent<SphereCollisionFollower>().currentBubble);
        Destroy(collidingBubbleScript.gameObject, 0.15f);
    }

    public void DestroyBubble(GameObject bubble)
    {
        Destroy(bubble, 0.1f);
    }

    public IEnumerator CreateNewMonster(GameObject monster)
    {
        // Palce it around in a 0.05 unit circle
        GameObject monsterPivot = Instantiate(monsterPivotPrefab, transform);

        monstersPivotsInside.Add(monsterPivot);
        //monsterPivot.transform.position = Random.insideUnitCircle * 0.05f;

        GameObject monsterTemp = Instantiate(monster, monsterPivot.transform);

        monstersInside.Add(monsterTemp);

        monsterTemp.transform.localPosition = new Vector3(0f, -0.27f, 0f);
        monsterTemp.transform.eulerAngles = new Vector3(0f, 0f, 0f);

        float lerpValue = 0f;

        while (lerpValue < 1f)
        {
            monsterTemp.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.2f, lerpValue);
            lerpValue += Time.deltaTime;
            yield return null;
        }


    }
}
