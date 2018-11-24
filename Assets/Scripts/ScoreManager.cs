using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    /// <summary>
    /// Singleton instance of this class.
    /// </summary>
    public static ScoreManager instance;

    /// <summary>
    /// The score text object;
    /// </summary>
    public Text scoreText;

    /// <summary>
    /// The current score of the player.
    /// </summary>
    public int currentScore;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("Attempted to instantiate two score manager instances.");
            Destroy(gameObject);
        }

        scoreText.text = currentScore.ToString(); // Initial score update
    }
    
    /// <summary>
    /// Increments the score by 1, and updates the score text.
    /// </summary>
    public void IncrementScore() {
        currentScore++;
        scoreText.text = currentScore.ToString();
    }
}
