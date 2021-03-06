﻿using UnityEngine;

/// <summary>
/// This is a class for the player controls.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

    /// <summary>
    /// Reference to the character controller.
    /// </summary>
    public CharacterController charControl;

    /// <summary>
    /// Reference to the camera transform.
    /// </summary>
    public Transform fpsCamera;

    /// <summary>
    /// Reference to the animator.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Sound effect for when the player is walking.
    /// </summary>
    public AudioClip walkSoundFX;

    /// <summary>
    /// Sound effect for when the player hits a wall;
    /// </summary>
    public AudioClip hitWallSoundFX;

    /// <summary>
    /// The player's audio source.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// How sensitive the camera rotation is.
    /// </summary>
    public float lookSensitivity = 2f;

    /// <summary>
    /// Speed of the player.
    /// </summary>
    public float speed = 2f;

    /// <summary>
    /// Flag for if a controller is plugged in.
    /// </summary>
    private bool _controllerPluggedIn;

    /// <summary>
    /// Used for determining speed of the player.
    /// </summary>
    private Vector3 _oldPos, _curPos;

    /// <summary>
    /// The current speed of the player;
    /// </summary>
    private float _currentSpeed;

    /// <summary>
    /// Collider array that holds nearby colliders.
    /// </summary>
    private Collider[] nearbyColliders;

	// Use this for initialization
	private void Start () {
        if (Input.GetJoystickNames().Length > 0 || Application.platform == RuntimePlatform.PS4) {
            _controllerPluggedIn = true;
        }
        _oldPos = transform.position;
        _curPos = transform.position;
        nearbyColliders = new Collider[10];
    }
	
	// Update is called once per frame
	private void Update () {
        // apply gravity
        charControl.Move(Vector3.down);

        // check for home input to reset position
        ResetInput();

        PlayerMovement();
        CameraRotation();

        // Calculate current speed
        _curPos = transform.position;
        _currentSpeed = (_curPos - _oldPos).magnitude / Time.deltaTime;

        // Set animation status
        if (_currentSpeed > 1f) {
            animator.SetBool("Walking", true);
        } else {
            animator.SetBool("Walking", false);
        }
        _oldPos = transform.position;

        // Check for the closest enemy object
        CheckNearbyColliders();
    }

    /// <summary>
    /// Resets the player to origin
    /// </summary>
    private void ResetInput() {
        float home = Input.GetAxis("Home");

        if (home > 0.1f) {
            this.transform.position = new Vector3(0f, 1.25f, 0f);
            this.transform.rotation = Quaternion.identity;
        }
    }

    /// <summary>
    /// Handles the player movement.
    /// </summary>
    private void PlayerMovement() {
        if (_controllerPluggedIn) {
            // d-pad axis variables
            float horzAxis = Input.GetAxis("DPadHorizontal");
            float vertAxis = -Input.GetAxis("DPadVertical");

            // Move accordingly to d-pad axis
            charControl.Move(transform.right * horzAxis * Time.deltaTime * speed);
            charControl.Move(transform.forward * vertAxis * Time.deltaTime * speed);

        } else {
            if (Input.GetKey(KeyCode.W)) { // Forward
                charControl.Move(transform.forward * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.S)) { // Backward
                charControl.Move(-transform.forward * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.A)) { // Left
                charControl.Move(-transform.right * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.D)) { // Right
                charControl.Move(transform.right * Time.deltaTime * speed);
            }
        }
    }

    /// <summary>
    /// Handles the first person camera rotation.
    /// </summary>
    private void CameraRotation() {
        // Camera rotation variables
        float horzRotation = 0f;
        float vertRotation = 0f;

        // Check if a controller is connected
        if (_controllerPluggedIn) {
            horzRotation = Input.GetAxis("Horizontal");
            vertRotation = Input.GetAxis("Vertical");
        } else {
            horzRotation = Input.GetAxis("Mouse X");
            vertRotation = Input.GetAxis("Mouse Y");
        }
        
        // Apply rotations
        transform.Rotate(new Vector3(0f, horzRotation, 0f) * lookSensitivity);
        TiltCamera(vertRotation);
    }

    /// <summary>
    /// Tilts the camera up or down.
    /// </summary>
    /// <param name="tiltDegrees">The amount to tilt the camera by in degrees</param>
    private void TiltCamera(float tiltDegrees) {
        // Camera rotation after tilting it
        Vector3 newRotation = fpsCamera.eulerAngles + (new Vector3(-tiltDegrees, 0f, 0f) * lookSensitivity);

        // The x value of the rotation normalized around 180 degrees 
        float newRotationX = newRotation.x > 180f ? newRotation.x - 360f : newRotation.x;

        // Clamp the rotation so that the player can't look too far up or down
        if (newRotationX > 60f) {
            newRotation.x = 60f;
        }
        if (newRotationX < -30f) {
            newRotation.x = 330f;
        }

        // Apply rotation to the camera
        fpsCamera.rotation = Quaternion.Euler(newRotation);
    }

    /// <summary>
    /// Plays the walking sound.
    /// </summary>
    public void PlayWalkSound() {
        SoundManager.instance.PlaySound(audioSource, walkSoundFX);
    }

    /// <summary>
    /// Checks for nearby colliders.
    /// Results are put into nearbyColliders array.
    /// </summary>
    private void CheckNearbyColliders() {
        Physics.OverlapSphereNonAlloc(transform.position, 15f, nearbyColliders, 1 << LayerMask.NameToLayer("Enemy")); // Get nearby colliders

        // Find the closest enemy distance
        float nearestDistance = float.MaxValue;
        foreach (Collider c in nearbyColliders) {
            if (c != null && !c.CompareTag("Aggro Area")) {
                float distance = Vector3.Distance(c.transform.position, transform.position);
                if (distance < nearestDistance) {
                    nearestDistance = distance;
                }
            }
        }

        SoundManager.instance.nearestDistance = nearestDistance;
    }

    /// <summary>
    /// Called when character controller hits another collider.
    /// </summary>
    private void OnControllerColliderHit(ControllerColliderHit collision) {
        // Check if collided with a wall, and if the wall collision sound isn't already playing
        if (collision.collider.CompareTag("Wall") && !SoundManager.instance.activeClips.Contains(hitWallSoundFX)) {
            SoundManager.instance.PlaySound(audioSource, hitWallSoundFX);
        }
    }

    /// <summary>
    /// Draws a red sphere.
    /// </summary>
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 15f);
    }
}
