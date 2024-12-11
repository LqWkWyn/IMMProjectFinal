using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    //public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    
    
    
    private bool controlsInverted = false; // Track if controls are inverted

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Check for control inversion toggle
        if (Input.GetKeyDown(KeyCode.C))
        {
            controlsInverted = !controlsInverted;
        }

        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Invert inputs if controls are inverted
        if (controlsInverted)
        {
            verticalInput *= -1;
            horizontalInput *= -1;
        }

        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);
        playerRb.AddForce(focalPoint.transform.right * horizontalInput * speed * Time.deltaTime);

        // Set pow indicator position to beneath player
        //powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            //powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        //powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
       private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Ensure your enemy has the "Enemy" tag
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

