using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongBall : MonoBehaviour {

	/// <summary>
	/// Velocity speed increase per sec
	/// </summary>
	[Tooltip("Speed up per sec")]
	public float SpeedUp;

	public float MaxSpeed;

	/// <summary>
	/// Travel velocity
	/// </summary>
	/// <returns></returns>
	[Tooltip("Travel Velocity")]
	public Vector3 Velocity;

	/// <summary>
	/// Rigidbody reference
	/// </summary>
	private Rigidbody _rigidBody;

	private float _sumSpeed;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		_rigidBody = GetComponent<Rigidbody>();

		// random angle in degrees
		float angle = new System.Random().Next(0, 360);
		
		// convert to rad
		angle *= Mathf.Deg2Rad;

		Velocity = Vector3.zero;
		
		Velocity.x = Mathf.Cos(angle);
		Velocity.z = Mathf.Sin(angle);
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{

		if (_sumSpeed < MaxSpeed)
		{
	
		// speed up the ball
		float scaledSpeedUp = SpeedUp * Time.deltaTime;

		_sumSpeed += scaledSpeedUp;
		
		// increase velocity of x and z based on their sign
		Velocity.x += Velocity.x < 0 ? -scaledSpeedUp : scaledSpeedUp;
		Velocity.z += Velocity.z < 0 ? -scaledSpeedUp : scaledSpeedUp;
		}

		// set rb's velocity
		_rigidBody.velocity = Velocity;
	}

	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		Velocity = Vector3.Reflect(Velocity, other.contacts[0].normal);
	}
}
