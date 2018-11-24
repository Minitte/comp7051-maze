using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    /// <summary>
    /// Singleton instance of this class.
    /// </summary>
    public static SoundManager instance;

    /// <summary>
    /// List of currently playing audio clips.
    /// </summary>
    public List<AudioClip> activeClips;

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
        source.PlayOneShot(clip);
        StartCoroutine(ActivateClip(clip));
    }

    /// <summary>
    /// Adds a clip to the active clips list.
    /// The clip is removed after the duration of its length.
    /// </summary>
    /// <param name="clip">The audio clip to add</param>
    public IEnumerator ActivateClip(AudioClip clip) {
        activeClips.Add(clip);
        yield return new WaitForSeconds(clip.length);
        activeClips.Remove(clip);
    }
}
