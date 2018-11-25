
using System;
using System.Collections.Generic;

[Serializable]
public class GameProgress
{
    /// <summary>
    /// if the maze is old    
    /// </summary>
    public bool oldMaze;

    /// <summary>
    /// Seed used for generating the maze
    /// </summary>
    public string mazeSeed;

    /// <summary>
    /// Player's Score
    /// </summary>
    public int score;

    /// <summary>
    /// Player position and info
    /// </summary>
    public EntitySave player;

    /// <summary>
    /// List of enemies
    /// </summary>
    public List<EnemySave> enemies;


}