using UnityEngine;

public class SoundManager : MonoBehaviour {

    /// <summary>
    /// Singleton instance of this class.
    /// </summary>
    public static SoundManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("Attempted to instantiate two sound manager instances.");
            Destroy(gameObject);
        }
    }
	
    /// <summary>
    /// Plays a sound clip.
    /// </summary>
    /// <param name="source">The source of the sound</param>
    /// <param name="clip">The sound clip to play</param>
    public void PlaySound(AudioSource source, AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
}
