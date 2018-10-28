using UnityEngine;

/// <summary>
/// This is a class for the player controls.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

    /// <summary>
    /// How sensitive the camera rotation is.
    /// </summary>
    public float lookSensitivity = 2f;

    /// <summary>
    /// Speed of the player.
    /// </summary>
    public float speed = 2f;

    /// <summary>
    /// Reference to the character controller.
    /// </summary>
    private CharacterController _charControl;

    /// <summary>
    /// Reference to the camera transform.
    /// </summary>
    private Transform _camera;

    /// <summary>
    /// Flag for if a controller is plugged in.
    /// </summary>
    private bool _controllerPluggedIn;

	// Use this for initialization
	private void Start () {
        _charControl = GetComponent<CharacterController>();
        _camera = GetComponentInChildren(typeof(Camera)).transform;

        // Check if there is a camera component attached to a child
        if (_camera == null) {
            Debug.LogWarning("Couldn't find a child with camera component attached to the player.");
        }

        if (Input.GetJoystickNames().Length > 0) {
            _controllerPluggedIn = true;
        }
    }
	
	// Update is called once per frame
	private void Update () {
        PlayerMovement();
        CameraRotation();
    }

    /// <summary>
    /// Handles the player movement.
    /// </summary>
    private void PlayerMovement() {
        if (_controllerPluggedIn) {
            // d-pad axis variables
            float horzAxis = Input.GetAxisRaw("DPadHorizontal");
            float vertAxis = -Input.GetAxisRaw("DPadVertical");

            // Move accordingly to d-pad axis
            _charControl.Move(transform.right * horzAxis * Time.deltaTime * speed);
            _charControl.Move(transform.forward * vertAxis * Time.deltaTime * speed);

        } else {
            if (Input.GetKey(KeyCode.W)) { // Forward
                _charControl.Move(transform.forward * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.S)) { // Backward
                _charControl.Move(-transform.forward * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.A)) { // Left
                _charControl.Move(-transform.right * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.D)) { // Right
                _charControl.Move(transform.right * Time.deltaTime * speed);
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
        
        // Rotate the player horizontally
        transform.Rotate(new Vector3(0f, horzRotation, 0f) * lookSensitivity);

        // Tilt the camera
        _camera.Rotate(new Vector3(-vertRotation, 0f, 0f) * lookSensitivity);
    }
}
