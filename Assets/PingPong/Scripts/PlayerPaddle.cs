using System;
using UnityEngine;

[Serializable]
public class PaddleControls
{
	public string UpKey;

	public string Downkey;
}

public class PlayerPaddle : MonoBehaviour {

	/// <summary>
	/// Speed for paddle movement
	/// </summary>
	[Tooltip("Speed for paddle movement")]
	public float Speed;

	/// <summary>
	/// Min point of movement
	/// </summary>
	[Tooltip("Min point of movement")]
	public Vector3 MinPoint;

	/// <summary>
	/// Max point of movement
	/// </summary>
	[Tooltip("Max point of movement")]
	public Vector3 MaxPoint;

	/// <summary>
	/// T/Time value between min and max points
	/// </summary>
	[Tooltip("T/Time value between min and max points")]
	public float TValue = 0.5f;

	/// <summary>
	/// Player who owns this
	/// </summary>
	[Tooltip("Player who owns this")]
	public int OwningSlot = -1;

	/// <summary>
	/// Paddle controls
	/// </summary>
	//[Tooltip("Paddle controls")]
	//public PaddleControls Controls; 

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		float tDist = Speed / Vector3.Distance(MaxPoint, MinPoint);
		tDist *= Time.deltaTime;
			
		float joy = Input.GetAxis("P" + (OwningSlot + 1) + "VertJoy");
		float key = Input.GetAxis("P" + (OwningSlot + 1) + "VertKey");

		float input = key != 0 ? key : joy;

		if (input != 0)
		{
			TValue += tDist * input;

			// max _t at 1
			TValue = TValue > 1f ? 1f : TValue;

			// min _t at 0
			TValue = TValue < 0f ? 0f : TValue;
		}

		transform.position = Vector3.Lerp(MinPoint, MaxPoint, TValue);
	}
}
