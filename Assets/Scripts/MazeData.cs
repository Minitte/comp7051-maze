using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeData : MonoBehaviour {

	/// <summary>
	/// Seed of this maze
	/// </summary>
	public string seed;

	/// <summary>
	/// List of enemies
	/// </summary>
	public List<Enemy> enemies;

	/// <summary>
	/// List of the maze tiles
	/// </summary>
	public List<MazeTile> tilesList;

	/// <summary>
	/// 2d array of tiles
	/// </summary>
	public MazeTile[,] tilesArray;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake() {
		enemies = new List<Enemy>();
		tilesList = new List<MazeTile>();
	}
}
