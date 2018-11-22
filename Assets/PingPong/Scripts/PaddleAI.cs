using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPaddle))]
public class PaddleAI : MonoBehaviour {

	/// <summary>
	/// List of balls
	/// </summary>
	/// <typeparam name="PingPongBall"></typeparam>
	/// <returns></returns>
	public List<PingPongBall> Balls = new List<PingPongBall>();

	/// <summary>
	/// Target position
	/// </summary>
	public Vector3 Target;

	private PlayerPaddle _paddle;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		// Subscript / register for Event
		GameEventManager.OnNewBall += AddBall;
		GameEventManager.OnRemoveBall += RemoveBall;

		_paddle = GetComponent<PlayerPaddle>();

		Target = gameObject.transform.position;
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		// find closest ball (highest danger)
		PingPongBall ball = FindClosestBall();

		// no ball to track
		if (ball == null)
		{
			return;
		}

		Target.z = ball.gameObject.transform.position.z;

		float tDist = _paddle.Speed / Vector3.Distance(_paddle.MaxPoint, _paddle.MinPoint);
		tDist *= Time.deltaTime;

		// don't move if already closely aligned
		if (Mathf.Abs(gameObject.transform.position.z - Target.z) < 1f)
		{
			return;
		}

		// move up
		if (gameObject.transform.position.z < Target.z)
		{
			_paddle.TValue += tDist;

			_paddle.TValue = _paddle.TValue > 1f ? 1f : _paddle.TValue;
		}

		// move down
		else if (gameObject.transform.position.z > Target.z)
		{
			_paddle.TValue -= tDist;

			// min _t at 0
			_paddle.TValue = _paddle.TValue < 0f ? 0f : _paddle.TValue;
		}
	}

	/// <summary>
	/// Finds the closest ball
	/// </summary>
	/// <returns></returns>
	private PingPongBall FindClosestBall()
	{
		PingPongBall closest = null;

		float minDist = 0f;

		foreach (PingPongBall b in Balls)
		{
			Vector3 diff = b.gameObject.transform.position - gameObject.transform.position;
			
			float distSquared = diff.sqrMagnitude;

			if (distSquared > minDist)
			{
				minDist = distSquared;
				closest = b;
			}
		}

		return closest;
	}

	/// <summary>
	/// Adds a ball to the list
	/// </summary>
	/// <param name="ball"></param>
	private void AddBall(PingPongBall ball)
	{
		Balls.Add(ball);
	} 

	/// <summary>
	/// Removes a ball from the list
	/// </summary>
	/// <param name="ball"></param>
	private void RemoveBall(PingPongBall ball)
	{
		Balls.Remove(ball);
	}
}
