using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameProgressManager : MonoBehaviour 
{
    /// <summary>
    /// Last updated save data
    /// </summary>
    public GameProgress currentSave;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Restores the state of the game
    /// </summary>
    public void RestoreFromSave(MazeData maze, GameObject localPlayer, ScoreManager score) 
    {
        // restore enemy states
        for (int i = 0; i < maze.enemies.Count; i++) {
            Enemy enemy = maze.enemies[i];
            EnemySave enemySave = currentSave.enemies[i];

            enemy.GetComponent<NavMeshAgent>().Warp(enemySave.position);

            if (enemySave.dead) {
                enemy.Die();
            }
        }

        // set player position
        localPlayer.transform.position = currentSave.player.position;

        // set score
        score.SetScore(currentSave.score);
    }

    /// <summary>
    /// Saves the game's state
    /// </summary>
    public void SaveGameState(MazeData maze, GameObject localPlayer, ScoreManager score) {
        currentSave.oldMaze = true;

        // Save enemies
        currentSave.enemies = new List<EnemySave>();

        for (int i = 0; i < maze.enemies.Count; i++) {
            EnemySave enemySave = new EnemySave();

            Enemy enemy = maze.enemies[i];

            enemySave.position = enemy.transform.position;
            enemySave.dead = enemy.dead; 
        }

        // Save player
        currentSave.player = new EntitySave();
        currentSave.player.position = localPlayer.transform.position;

        // save score
        currentSave.score = score.currentScore;

        // save seed
        currentSave.mazeSeed = maze.seed;

    }
}
