using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeDoor : MonoBehaviour {

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player")) 
		{
			GameObject.Find("Maze Game Manager").GetComponent<dpaw.bcit.c7051.maze.GameManager>().SaveState();

			SceneManager.LoadScene("Game");
		}
	}
}