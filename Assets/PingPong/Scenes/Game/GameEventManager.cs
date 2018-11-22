using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour {
	
	public delegate void BallEvent(PingPongBall ball);

	public static event BallEvent OnNewBall;

	public static event BallEvent OnRemoveBall;

	public delegate void GoalEvent(int scoringTeam);

	public static event GoalEvent OnGoal;

	/// <summary>
	/// Triggers a OnNewBall event
	/// </summary>
	public void BroadcastBallSpawnEvent(PingPongBall ball)
	{
		if (OnNewBall != null)
		{
			OnNewBall(ball);
		}
	}

	/// <summary>
	/// Triggers a OnRemoveBall event
	/// </summary>
	public void BroadcastBallRemoveEvent(PingPongBall ball)
	{
		if (OnRemoveBall != null)
		{
			OnRemoveBall(ball);
		}
	}

	/// <summary>
	/// Triggers a OnGoal event
	/// </summary>
	/// <param name="scoringTeam"></param>
	public void BroadcastGoalEvent(int scoringTeam)
	{
		if (OnGoal != null)
		{
			OnGoal(scoringTeam);
		}
	}
}
