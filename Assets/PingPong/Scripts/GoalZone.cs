using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour {

	/// <summary>
	/// Event Manager
	/// </summary>
	public GameEventManager EventMngr;

	public int OwningSlot;

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		// not a ball, don't care
		if (other.gameObject.tag != "Ball")
		{
			return;
		}

		// trigger score event
		int scoringSlot = OwningSlot == 1 ? 0 : 1;
		EventMngr.BroadcastGoalEvent(scoringSlot);

		// destory ball and trigger event
		PingPongBall ball = other.gameObject.GetComponent<PingPongBall>();
		EventMngr.BroadcastBallRemoveEvent(ball);
		GameObject.Destroy(other.gameObject);
	}
}
