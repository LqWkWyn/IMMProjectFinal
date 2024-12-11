using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;        // Reference to the UI text component
    public TextMeshProUGUI highScoreText;    // Reference to the high score UI text component
    private SpawnManagerX spawnManager;       // Reference to the spawn manager
    private int highScore = 0;               // Tracks the highest wave reached

    void Start()
    {
        // Find the spawn manager in the scene
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();
        
        // Load the previous high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
    }

    void Update()
    {
        // Update current wave/enemy count
        if (scoreText != null)
        {
            scoreText.text = "Wave: " + spawnManager.waveCount.ToString();
        }

        // Check if current wave is higher than high score
        if (spawnManager.waveCount > highScore)
        {
            highScore = spawnManager.waveCount;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreText();
        }
    }

    void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }



    // Call this when game resets or player dies
    public void ResetCurrentScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Wave: 1";
        }
    }

    // Call this to reset high score (optional)
    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();
        UpdateHighScoreText();
    }
}
