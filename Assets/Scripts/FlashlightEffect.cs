using System.Collections.Generic;
using UnityEngine;

public class FlashlightEffect : MonoBehaviour {

    /// <summary>
    /// Reference to the main camera.
    /// </summary>
    public Transform mainCamera;

    /// <summary>
    /// Flag for if this effect is active or not.
    /// </summary>
    private bool _enabled;

    private void Start() {
        Shader.SetGlobalVector("_FlashlightPoint", new Vector3(0, 0, 0)); // Hide flashlight
    }

    private void Update() {
        if (Input.GetButtonDown("Flashlight")) {
            if (_enabled) {
                Shader.SetGlobalVector("_FlashlightPoint", new Vector3(0, 0, 0)); // Hide flashlight
            }
            _enabled = !_enabled;
        }

        if (_enabled) {
            // Camera forward ray
            Ray ray = new Ray(mainCamera.position, mainCamera.forward);
            RaycastHit rayHitInfo;

            if (Physics.Raycast(ray, out rayHitInfo, 1000, ~(1 << LayerMask.NameToLayer("Enemy")))) {
                Shader.SetGlobalVector("_FlashlightPoint", rayHitInfo.point);
                Shader.SetGlobalVector("_FlashlightDirection", mainCamera.transform.forward);
            }
        }
    }
}
