using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This is a class for maze generation.
/// </summary>
public class MazeGenerator : MonoBehaviour {

	[Header("Prefabs")]

	/// <summary>
	/// Prefab of a tile
	/// </summary>
	public GameObject tilePrefab;

	/// <summary>
	/// Prefab of the parent for the maze
	/// </summary>
	public GameObject mazeParentPrefab;

	/// <summary>
	/// Prefab of an enemy to randomly spawn in
	/// </summary>
	public GameObject enemyPrefab;

	[Header("Others")]

	public GameObject currentMaze;

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

		StartCoroutine(CreateMaze());
	}

	/// <summary>
	/// Creates a maze
	/// </summary>
	/// <returns></returns>
	public IEnumerator CreateMaze() {
		currentMaze = Instantiate(mazeParentPrefab);

		// Create the maze
		FillMaze();
		GeneratePath();

		// Starting and exit points
		_tiles[0, 0].walls[2].GetComponent<Renderer>().enabled = false;
		_tiles[mazeSize - 1, mazeSize - 1].walls[0].GetComponent<Renderer>().enabled = false;

		// wait 1 frame for colliders to update
		yield return new WaitForEndOfFrame();

		// build nav mesh
		currentMaze.GetComponent<NavMeshSurface>().BuildNavMesh();

		// spawn enemies in
		SpawnEnemies();
	}

	/// <summary>
	/// Fills the maze with tiles.
	/// Each tile will already be surrounded with 4 walls.
	/// </summary>
	private void FillMaze() {
		float tileWidth = tilePrefab.transform.localScale.x * 2;
		float tileLength = tilePrefab.transform.localScale.z * 2;

		for (int i = 0; i < mazeSize; i++) {
			for (int j = 0; j < mazeSize; j++) {
				_tiles[i,j] = Instantiate(tilePrefab, currentMaze.transform).GetComponent<MazeTile>();
				// Set the coordinates here
				_tiles[i,j].coord = new Coordinate(i, j);

				_tiles[i,j].transform.Translate(i * tileWidth, 0, j * tileLength);
			}
		}
	}

	/// <summary>
	/// Generates the maze path using DFS algorithm.
	/// </summary>
	private void GeneratePath() {
		// Variables for DFS
		Stack<MazeTile> tileStack = new Stack<MazeTile>();
		bool[,] visited = new bool[mazeSize, mazeSize];
		System.Random random = new System.Random();

		// Initialize DFS
		tileStack.Push(_tiles[random.Next(0, mazeSize), random.Next(0, mazeSize)]);

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
				if (OutOfBounds(c)) {
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

	/// <summary>
	/// Removes extra walls in the maze tiles
	/// </summary>
	private void RemoveExtraWalls() {
		// check board pattern
		for (int x = 0; x < mazeSize; x++) {
			for (int z = 0; z < mazeSize; z++) {
				MazeTile tile = _tiles[x, z];

				// perform for all walls
				for (int dir = 0; dir < 4; dir++) {
					Coordinate coordDir = Coordinate.GetCoordinate(dir);
					Coordinate coord = tile.coord + coordDir;

					// check if coordinate points out of bounds
					if (OutOfBounds(coord)) {
						continue;
					}

					// no wall to break;
					if (tile.walls[dir] == null) {
						continue;
					}

					MazeTile neighbour = _tiles[coord.x, coord.z];

					// check other wall
					if (neighbour.walls[(dir + 2) % 4] == null) {
						continue;
					}

					// remove one wall because there are two
					tile.BreakWall(dir);
				}
			}
		}
	}

	private void SpawnEnemies() {
		System.Random random = new System.Random();

		for (int i = 0; i < (int)mazeSize; i++) {
			int x = random.Next(0, mazeSize);
			int z = random.Next(0, mazeSize);

			// min distances from entrance
			if (new Vector2(x, z).magnitude < 4f) {
				i--;
				continue;
			}

			Vector3 pos = _tiles[x, z].transform.position;

			pos.y += 2;

			GameObject enemyGO = Instantiate(enemyPrefab, pos, Quaternion.identity); 

			Enemy enemyScript = enemyGO.GetComponent<Enemy>();

			// setting up patrol destinations
			enemyScript.patrolList = new Vector3[2];

			int patrolX = random.Next(0, mazeSize);
			int patrolZ = random.Next(0, mazeSize);

			enemyScript.patrolList[0] = _tiles[patrolX, patrolZ].transform.position;

			enemyScript.patrolList[1] = _tiles[x, z].transform.position;
		}
	}

	/// <summary>
	/// Checks if the coordinate is out of bounds
	/// </summary>
	/// <param name="coord"></param>
	/// <returns></returns>
	private bool OutOfBounds(Coordinate c) {
		return c.x < 0 || c.z < 0 || c.x >= mazeSize || c.z >= mazeSize;
	}
}
