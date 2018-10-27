using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroArea : MonoBehaviour {

	/// <summary>
	/// event delegate
	/// </summary>
	/// <param name="aggro"></param>
	public delegate void AggroEvent(GameObject aggro);

	/// <summary>
	/// Event for finding a target
	/// </summary>
	public event AggroEvent OnFoundTarget;

	/// <summary>
	/// Current aggro Target
	/// </summary>
	public GameObject currentTarget;

	/// <summary>
	/// Tags to watch for
	/// </summary>
	public string[] tags;

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		if (currentTarget != null) {
			Vector3 direciton = currentTarget.transform.position - gameObject.transform.position;

			direciton.Normalize();

			Ray ray = new Ray(gameObject.transform.position, direciton);

			if (Physics.Raycast(ray, 1000f, 1 << 15)) {
				currentTarget = null;
			}
		}
	}

	/// <summary>
	/// OnTriggerStay is called once per frame for every Collider other
	/// that is touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerStay(Collider other) {
		if (currentTarget != null || !ContainsTag(other.tag)) {
			return;
		}

		Vector3 direciton = other.transform.position - gameObject.transform.position;

		direciton.Normalize();

		Ray ray = new Ray(gameObject.transform.position, direciton);

		if (Physics.Raycast(ray, 1000f, 1 << 15)) {
			return;
		}

		currentTarget = other.gameObject;

		if (OnFoundTarget != null) {
			OnFoundTarget(other.gameObject);
		}
	}

	/// <summary>
	/// Checks if the tag is in the list
	/// </summary>
	/// <param name="tag"></param>
	/// <returns></returns>
	private bool ContainsTag(string tag) {
		for (int i = 0; i < tags.Length; i++) {
			if (tags[i] == tag) {
				return true;
			}
		}

		return false;
	}
}
