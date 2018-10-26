using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a class for maze generation.
/// </summary>
public class MazeGenerator : MonoBehaviour {

	[Header("Prefabs")]

	/// <summary>
	/// Prefab of a tile
	/// </summary>
	public GameObject tilePrefab;

	[Header("Others")]

	/// <summary>
	/// Size of the maze
	/// </summary>
	public int mazeSize;

	private MazeTile[,] _tiles;
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		_tiles = new MazeTile[mazeSize, mazeSize];
		
		// Create the maze
		FillMaze();
		GeneratePath();

		// Starting and exit points
		_tiles[0, 0].BreakWall(2);
		_tiles[mazeSize - 1, mazeSize - 1].BreakWall(0);
	}

	/// <summary>
	/// Fills the maze with tiles.
	/// Each tile will already be surrounded with 4 walls.
	/// </summary>
	public void FillMaze() {
		float tileWidth = tilePrefab.transform.localScale.x * 2;
		float tileLength = tilePrefab.transform.localScale.z * 2;

		for (int i = 0; i < mazeSize; i++) {
			for (int j = 0; j < mazeSize; j++) {
				_tiles[i,j] = Instantiate(tilePrefab).GetComponent<MazeTile>();
				// Set the coordinates here
				_tiles[i,j].coord = new Coordinate(i, j);

				_tiles[i,j].transform.Translate(i * tileWidth, 0, j * tileLength);
			}
		}
	}

	/// <summary>
	/// Generates the maze path using DFS algorithm.
	/// </summary>
	public void GeneratePath() {
		// Variables for DFS
		Stack<MazeTile> tileStack = new Stack<MazeTile>();
		bool[,] visited = new bool[mazeSize, mazeSize];
		System.Random random = new System.Random();

		// Initialize DFS
		tileStack.Push(_tiles[random.Next(0, mazeSize), random.Next(0, mazeSize)]);
		MazeTile startingTile = tileStack.Peek();

		// Continue exploring paths until there are none remaining
		while (tileStack.Count > 0) {
			// Visit the top element of the stack
			MazeTile currentTile = tileStack.Peek();
			visited[currentTile.coord.x, currentTile.coord.z] = true;

			// Direction variables
			int direction = random.Next(0, 4);
			Coordinate c = null;
			bool foundDirection = false;

			// Attempt to find a valid direction to traverse
			for (int tries = 0; tries < 4; tries++) {
				c = currentTile.coord + Coordinate.GetCoordinate(direction);
				
				// check bounds
				if (c.x < 0 || c.z < 0 || c.x >= mazeSize || c.z >= mazeSize) {
					direction = (direction + 1) % 4;
					continue;
				}

				if (visited[c.x, c.z] == true) {
					direction = (direction + 1) % 4;
					continue;
				}
				
				// Found a direction
				foundDirection = true;
				break;
			}

			// check if found a direction
			if (foundDirection) {
				currentTile.BreakWall(direction);
				MazeTile t = _tiles[c.x, c.z];

				// Break the wall from the opposite side too
				t.BreakWall((direction + 2) % 4);

				tileStack.Push(t);
			} else {
				// Go back
				tileStack.Pop();
			}
		}
	}
}
