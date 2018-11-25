using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dpaw.bcit.c7051.maze
{

	public class GameManager : MonoBehaviour 
	{

		/// <summary>
		/// game progress manager to restore with
		/// </summary>
		public GameProgressManager gameProgressManager;

		/// <summary>
		/// maze generator to generate maze with 
		/// </summary>
		public MazeGenerator mazeGenerator;

		/// <summary>
		/// Score manager to pull scores from
		/// </summary>
		public ScoreManager scoreManager;

		/// <summary>
		/// Start is called on the frame when a script is enabled just before
		/// any of the Update methods is called the first time.
		/// </summary>
		void Start()
		{
			if (!gameProgressManager.currentSave.oldMaze) {
				mazeGenerator.BeginMazeGeneration("seed" + new System.Random().Next(0, 9999999));
			} else {
				mazeGenerator.OnGeneratingComplete += RestoreFromSave;

				mazeGenerator.BeginMazeGeneration(gameProgressManager.currentSave.mazeSeed);
			}
		}

		/// <summary>
		/// Restores the game state
		/// </summary>
		/// <param name="maze"></param>
		private void RestoreFromSave(MazeData maze) 
		{
			GameObject player = GameObject.Find("Player");

			gameProgressManager.RestoreFromSave(maze, player, scoreManager);
		}
	}
}