using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    /// <summary>
    /// Singleton instance of this class.
    /// </summary>
    public static SoundManager instance;

    /// <summary>
    /// The audio source attached to this gameobject.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// Music that's played during the day.
    /// </summary>
    public AudioClip dayMusic;

    /// <summary>
    /// Music that's played during the night.
    /// </summary>
    public AudioClip nightMusic;

    /// <summary>
    /// The distance between the player and the nearest enemy.
    /// </summary>
    public float nearestDistance;

    /// <summary>
    /// List of currently playing audio clips.
    /// </summary>
    public List<AudioClip> activeClips;

    /// <summary>
    /// The current music clip that should be playing.
    /// Not always the active clip that's on the audio source.
    /// </summary>
    private AudioClip _currentMusic;

    /// <summary>
    /// True if daytime, false if nighttime.
    /// </summary>
    private bool _day;

    /// <summary>
    /// True if fog is toggled, false otherwise.
    /// </summary>
    private bool _fog;

    /// <summary>
    /// Property variable for day.
    /// </summary>
    public bool Day {
        set {
            _day = value;
            _currentMusic = _day ? dayMusic : nightMusic;

            // Switch music clip
            if (audioSource.isPlaying && !audioSource.clip.Equals(_currentMusic)) {
                PlayMusic(_day ? dayMusic : nightMusic);
            }
        }
    }

    /// <summary>
    /// Property variable for fog.
    /// </summary>
    public bool Fog {
        set { _fog = value; }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("Attempted to instantiate two sound manager instances.");
            Destroy(gameObject);
        }

        // Play day music on startup
        _day = true;
        PlayMusic(dayMusic);
    }

    private void Update() {
        if (Input.GetButtonDown("ToggleMusic")) {
            ToggleMusic();
        }

        AdjustMusicVolume();
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
    /// Plays a music clip.
    /// </summary>
    /// <param name="clip">The music clip to play</param>
    public void PlayMusic(AudioClip clip) {
        audioSource.Stop(); // Stop current music
        audioSource.clip = clip;
        audioSource.Play();
    }

    /// <summary>
    /// Toggles the music on or off.
    /// </summary>
    public void ToggleMusic() {
        if (audioSource.isPlaying) {
            audioSource.Pause();
        } else {
            audioSource.UnPause();

            // Check if the audio clip was changed while paused
            if (!audioSource.clip.Equals(_currentMusic)) {
                PlayMusic(_day ? dayMusic : nightMusic);
            }
        }
    }

    public void AdjustMusicVolume() {
        float volume = 1f;

        // Adjust volume based on distance to the closest enemy
        volume *= 1f / nearestDistance;

        // Fog halves the volume
        if (_fog) {
            volume *= 0.5f;
        }

        audioSource.volume = volume; // Set volume
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
