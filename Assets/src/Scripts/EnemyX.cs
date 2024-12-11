using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    public NavMeshAgent agent;
    private SpawnManagerX spawnManagerX;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        spawnManagerX = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();
        speed = spawnManagerX.enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 hitpoint = player.transform.position;
        agent.SetDestination(hitpoint);
    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with player, reset the game state
        if (other.gameObject.CompareTag("Player"))
        {   
            // Destroy all enemies
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            
            // Reset wave count and spawn new wave
            spawnManagerX.waveCount = 1;
            spawnManagerX.SpawnNewWave();
            
            // Reset player position
            ResetPlayerPosition();
        }
    }

    void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
