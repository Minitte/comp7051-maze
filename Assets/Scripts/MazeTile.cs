using UnityEngine;
/// <summary>
/// This is a class for maze tiles.
/// </summary>
public class MazeTile : MonoBehaviour {


	/// <summary>
	/// list of walls of the tile
	/// </summary>
	public GameObject[] walls;

	/// <summary>
	/// The coordinate of the tile.
	/// </summary>
	public Coordinate coord;

	/// <summary>
	/// Breaks a wall in the input direction.
	/// </summary>
	/// <param name="index">The direction of the wall to break</param>
	public void BreakWall(int index) {
		if (walls[index] != null) {
			Destroy(walls[index]);
		} else {
			Debug.Log("Tried to destroy a nonexistent wall");
		}
	}
}
