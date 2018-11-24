using UnityEngine;

public class Ball : MonoBehaviour {

    /// <summary>
    /// The sound effect to play when this ball collides with something.
    /// </summary>
    public AudioClip soundFX;

    /// <summary>
    /// The audio source of the ball.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// Flag that is used to prevent more than one OnTriggerEnter call per fixed update.
    /// </summary>
    private bool _checking;

    /// <summary>
    /// Called when this gameobject's collider hits another collider.
    /// </summary>
    /// <param name="other">The other collider involved in the collision</param>
    private void OnCollisionEnter(Collision other) {
        SoundManager.instance.PlaySound(audioSource, soundFX); // Play sound effect
    }

    private void FixedUpdate() {
        _checking = false;
    }

    /// <summary>
    /// Called when this gameobject's collider enters a trigger.
    /// </summary>
    /// <param name="other">The trigger that was hit</param>
    private void OnTriggerEnter(Collider other) {
        // Enforce mutual exclusion
        if (!_checking) {
            _checking = true;
        } else {
            return;
        }

        SoundManager.instance.PlaySound(audioSource, soundFX); // Play sound effect

        // Check if collided with enemy
        if (other.gameObject.CompareTag("Enemy") && _checking) {
            // Increment score and destroy the ball immediately
            ScoreManager.instance.IncrementScore();
            Destroy(gameObject);
        }
    }
}
