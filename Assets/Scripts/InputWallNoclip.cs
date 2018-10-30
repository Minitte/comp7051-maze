using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWallNoclip : MonoBehaviour {

	/// <summary>
	/// Coolider component
	/// </summary>
	private Collider _collider;

	/// <summary>
	/// Noclip toggle is on cooldown
	/// </summary>
	private bool _cooldown;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		_collider = GetComponent<Collider>();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		float noclip = Input.GetAxis("NoClip");

		if (_cooldown) {
			if (noclip > 0.1f) {
				return;
			} else {
				_cooldown = false;
			}
		}

		if (noclip > 0.1f) {
			_collider.enabled = !_collider.enabled;
			_cooldown = true;
		} 
	}
}
