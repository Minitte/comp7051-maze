using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

	/// <summary>
	/// Aggro area script and object
	/// </summary>
	public AggroArea aggroArea;

	/// <summary>
	/// nav agent 
	/// </summary>
	private NavMeshAgent _agent;	

	/// <summary>
	/// target to chase
	/// </summary>
	private GameObject _target;	

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		_agent = GetComponent<NavMeshAgent>();

		aggroArea.OnFoundTarget += Chase;
		aggroArea.OnTargetLost += StopChasing;
	}

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate() {
		if (_target != null) {
			_agent.SetDestination(_target.transform.position);
		}
	}

	/// <summary>
	/// Makes the enemy chase the target
	/// </summary>
	/// <param name="target"></param>
	public void Chase(GameObject target) {
		_target = target;
	}

	/// <summary>
	/// Stops chasing the current target
	/// </summary>
	/// <param name="target"></param>
	public void StopChasing(GameObject target) {
		_target = null;
	}
}
