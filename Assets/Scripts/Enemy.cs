﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

	public enum AIState {
		PATROL,
		CHASE
	}

	[Header("Component References")]

	/// <summary>
	/// Aggro area script and object
	/// </summary>
	public AggroArea aggroArea;

	/// <summary>
	/// Animator Component
	/// </summary>
	public Animator animator;

	[Header("AI Properties")]

	/// <summary>
	/// the AI's current state
	/// </summary>
	public AIState state;

	/// <summary>
	/// Delay for reactions
	/// </summary>
	public float reactionDelay;

	/// <summary>
	/// The walking speed for when not chasing
	/// </summary>
	public float walkSpeed;

	/// <summary>
	/// The chasing speed for when target found
	/// </summary>
	public float chaseSpeed;

	/// <summary>
	/// List of positions to patrol between
	/// </summary>
	public Vector3[] patrolList;

	/// <summary>
	/// if the enemy is dead
	/// </summary>
	/// <value></value>
	public bool dead { get { return _dead; } }

	/// <summary>
	/// nav agent 
	/// </summary>
	private NavMeshAgent _agent;	

	/// <summary>
	/// target to chase
	/// </summary>
	private GameObject _target;	

	/// <summary>
	/// Time tracking for ai reactions
	/// </summary>
	private float _time;

	/// <summary>
	/// Index for patrol destination list
	/// </summary>
	private int _patrolIndex;

    /// <summary>
    /// Flag to represent if this enemy is dead.
    /// </summary>
    private bool _dead;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		_agent = GetComponent<NavMeshAgent>();

		aggroArea.OnFoundTarget += Chase;
		aggroArea.OnTargetLost += StopChasing;

		NextPatrolDestination();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		animator.SetFloat("Speed", _agent.velocity.magnitude);
		animator.gameObject.transform.localPosition = Vector3.zero;
	}

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate() {
        if (_dead) {
			_agent.isStopped = true;
            return;
        }
		switch (state) {

			// chasing a player
			case AIState.CHASE:
			if (_target != null) {
				_time += Time.deltaTime;

				if (_time > reactionDelay) {
					_time = 0;

					_agent.SetDestination(_target.transform.position);
				}

			} else {
				SetToPatrol();
			}
			break;

			case AIState.PATROL:
			// check if AI has reached it's destination 
			if (!_agent.pathPending) {
				if (_agent.remainingDistance <= _agent.stoppingDistance) {
					if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f) {
						NextPatrolDestination();
					}
				}
			}
			break;
		}
	}

	/// <summary>
	/// Makes the enemy chase the target
	/// </summary>
	/// <param name="target"></param>
	public void Chase(GameObject target) {
		_target = target;
		_agent.SetDestination(_target.transform.position);

		SetToChase();
	}

	/// <summary>
	/// Stops chasing the current target
	/// </summary>
	/// <param name="target"></param>
	public void StopChasing(GameObject target) {
		SetToPatrol();
	}

	/// <summary>
	/// Sets the ai for patroling
	/// </summary>
	private void SetToPatrol() {
		state = AIState.PATROL;

		_target = null;

		_agent.speed = walkSpeed;

		_agent.SetDestination(patrolList[_patrolIndex]);
	}

	/// <summary>
	/// Sets the ai to chasing
	/// </summary>
	private void SetToChase() {
		state = AIState.CHASE;

		_agent.speed = chaseSpeed;
	}

	/// <summary>
	/// Goes to the next patrol destination
	/// </summary>
	private void NextPatrolDestination() {
		_patrolIndex = (_patrolIndex + 1) % patrolList.Length;

		_agent.SetDestination(patrolList[_patrolIndex]);
	}

    /// <summary>
    /// Kills this enemy.
    /// </summary>
    public void Die() {
		_agent = GetComponent<NavMeshAgent>();
		
        // Unsubscribe from events
        aggroArea.OnFoundTarget -= Chase;
        aggroArea.OnTargetLost -= StopChasing;

        animator.SetTrigger("Die"); // Play death animation
        GetComponent<Collider>().enabled = false; // Disable collider

        // Reset path
        _agent.ResetPath();

        _dead = true;
    }
}
