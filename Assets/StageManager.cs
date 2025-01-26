using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Obstacle[] possibleObstacles;  // Array of obstacle prefabs to instantiate

    [Serializable]
    public struct Obstacle
    {
        public GameObject obstaclePrefab;
        public int chanceOfspawn;
    }

    private List<GameObject> obstaclePrefabs;

    [SerializeField] private float initialCooldown = 5f;    // Initial spawn interval
    [SerializeField] private float cooldownDecrement = 0.1f; // Amount to reduce cooldown after each spawn
    [SerializeField] private float minCooldown = 2f;        // Minimum cooldown time to prevent too frequent spawns

    private float currentCooldown; // Current cooldown time for the next spawn
    private float elapsedTime;     // Tracks the elapsed time

    void Start()
    {
        // Set the initial cooldown to the specified value

        obstaclePrefabs = new List<GameObject>();

        foreach(Obstacle obstacle in possibleObstacles)
        {
            for(int i = 0; i < obstacle.chanceOfspawn; i++)
            {
                GameObject randomObstacle = Instantiate(obstacle.obstaclePrefab);
                obstaclePrefabs.Add(randomObstacle);
                randomObstacle.SetActive(false);
            }
        }


        currentCooldown = initialCooldown;
        elapsedTime = 0f;
    }

    void Update()
    {
        // Count up the elapsed time
        elapsedTime += Time.deltaTime;

        // Check if the cooldown has finished and spawn the obstacle
        if (elapsedTime >= currentCooldown)
        {
            SpawnObstacle();

            // Reset the timer
            elapsedTime = 0f;

            // Reduce the cooldown but ensure it doesn't go below the minimum
            currentCooldown = Mathf.Max(currentCooldown - cooldownDecrement, minCooldown);
        }
    }

    // Function to spawn a random obstacle at the current position
    void SpawnObstacle()
    {
        obstaclePrefabs[UnityEngine.Random.Range(0,  obstaclePrefabs.Count)].SetActive(true);
    }
}
