using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

	public static List<PlayerInfo> Players = new List<PlayerInfo>();

	/// <summary>
	/// Flag for AI player
	/// </summary>
	[Tooltip("Flag for AI player")]
	public bool IsAI;

	/// <summary>
	/// flag for playing
	/// </summary>
	[Tooltip("Flag for playing")]
	public bool IsPlaying;

	/// <summary>
	/// Game position slot
	/// </summary>
	[Tooltip("Game position slot")]
	public int Slot;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
		gameObject.name = "Player " + Slot;
		Players.Add(this);
	}

	public void SetAI(bool isAI)
	{
		this.IsAI = isAI;
	}
}
