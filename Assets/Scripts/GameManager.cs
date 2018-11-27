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
			// PlayerPrefs.SetString("Save", "");
			gameProgressManager.LoadSave();

			if (!gameProgressManager.loaded) {
				mazeGenerator.BeginMazeGeneration("seed" + new System.Random().Next(0, 9999999));
			} else {
				mazeGenerator.OnGeneratingComplete += RestoreFromSave;

				mazeGenerator.BeginMazeGeneration(gameProgressManager.currentSave.mazeSeed);
			}
		}

		/// <summary>
		/// This function is called when the MonoBehaviour will be destroyed.
		/// </summary>
		void OnDestroy()
		{
			mazeGenerator.OnGeneratingComplete -= RestoreFromSave;
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

		/// <summary>
		/// Saves the maze's game state
		/// </summary>
		public void SaveState() 
		{
			GameObject player = GameObject.Find("Player");

			gameProgressManager.SaveGameState(mazeGenerator.currentMaze, player, scoreManager);
		}
	}
}