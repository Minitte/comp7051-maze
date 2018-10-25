using System.Collections.Generic;
using UnityEngine;

public class MazeTile : MonoBehaviour {

	public Transform wallParent;

	/// <summary>
	/// list of walls of the tile
	/// </summary>
	public List<GameObject> walls;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		walls = new List<GameObject>();
	}

	/// <summary>
	/// Adds a wall of the tile
	/// </summary>
	/// <param name="wall"></param>
	public void AddWall(GameObject wall) {
		walls.Add(wall);

		wall.transform.SetParent(wallParent, false);
	}
}
