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
    /// Called when this gameobject's collider hits another collider.
    /// </summary>
    /// <param name="collision">The other collider involved in the collision</param>
    private void OnCollisionEnter(Collision collision) {
        SoundManager.instance.PlaySound(audioSource, soundFX); // Play sound effect
    }
}
