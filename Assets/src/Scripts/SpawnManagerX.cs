using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 20;
    private float spawnZMin = 10; // set min spawn Z
    private float spawnZMax = 30; // set max spawn Z

    public int powerupCount;
    public int waveCount = 1; // Start with wave 1
    public float enemySpeed = 50;
    private bool isSpawning = false;

    public GameObject player; 

    void Start()
    {
        // Spawn initial wave
        SpawnNewWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning)
        {
            // Count powerups instead of enemies
            powerupCount = GameObject.FindGameObjectsWithTag("Powerup").Length;

            if (powerupCount == 0)
            {
                isSpawning = true;
                ClearEnemies(); // Clear existing enemies before spawning new ones
                SpawnNewWave();
                isSpawning = false;
            }
        }
    }

    // Generate random spawn position for powerups and enemy balls
    public Vector3 GenerateSpawnPosition()
    {
        Vector3 spawnPosition;
        do
        {
            float xPos = Random.Range(-spawnRangeX, spawnRangeX);
            float zPos = Random.Range(spawnZMin, spawnZMax);
            spawnPosition = new Vector3(xPos, 1, zPos);
        } while (Vector3.Distance(spawnPosition, player.transform.position) < 5f); // Ensure it's at least 5 units away from the player

        return spawnPosition;
    }

    public void SpawnNewWave()
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end
        
        // Spawn 3 powerups
        for (int i = 0; i < 3; i++)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }
        
        // Spawn enemies based on wave number (wave 1 = 1 enemy, wave 2 = 2 enemies, etc.)
        for (int i = 0; i < waveCount; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
        
        Debug.Log("Wave " + waveCount + " started with " + waveCount + " enemies");
        
        // Increment wave count for the next wave
        waveCount++;
    }

    // Method to clear all enemies from the scene
    private void ClearEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Ensure enemies are tagged as "Enemy"
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}