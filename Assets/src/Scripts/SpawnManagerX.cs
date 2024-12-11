using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 15;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    public int powerupCount;
    public int waveCount = 1;
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
                SpawnNewWave();
                isSpawning = false;
            }
        }
    }

    // Generate random spawn position for powerups and enemy balls
    public Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
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
        waveCount++;
    }
}
