using UnityEngine;

/// <summary>
/// This is a class for displaying the fog effect.
/// </summary>
[ExecuteInEditMode]
public class Fog : MonoBehaviour {

    /// <summary>
    /// The fog material.
    /// </summary>
    public Material fogMaterial;

    /// <summary>
    /// Reference to the camera attached to this gameobject.
    /// </summary>
    public Camera _camera;

    /// <summary>
    /// Flag for if the fog is enabled or not.
    /// </summary>
    private bool _enabled;

    private void Start() {
        _camera = GetComponent<Camera>();
        if (_camera == null) {
            Debug.LogWarning("Couldn't find a camera attached with this Fog component.");
        }

        // Enable generation of a depth texture
        _camera.depthTextureMode = DepthTextureMode.Depth;
    }

    private void Update() {
        // Toggling the fog effect
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown("joystick button 3")) {
            ToggleFog();
        }
    }

    /// <summary>
    /// Toggles the fog on or off.
    /// </summary>
    private void ToggleFog() {
        // Adjust sight radius
        if (_enabled) {
            _camera.farClipPlane = 100f;
        } else {
            _camera.farClipPlane = 5f;
        }
        
        _enabled = !_enabled; // Toggle enabled flag
    }

    /// <summary>
    /// Called after all images are rendered.
    /// Used for postprocessing.
    /// </summary>
    /// <param name="source">The source render texture</param>
    /// <param name="destination">The destination render texture</param>
    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        if (_enabled) {
            // Apply fog material onto destination texture
            Graphics.Blit(source, destination, fogMaterial);
        } else {
            Graphics.Blit(source, destination); // Default rendering
        }
    }
}
