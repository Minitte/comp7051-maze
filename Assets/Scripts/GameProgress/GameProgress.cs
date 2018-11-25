
using System;
using System.Collections.Generic;

[Serializable]
public class GameProgress
{
    /// <summary>
    /// Seed used for generating the maze
    /// </summary>
    public int mazeSeed;

    /// <summary>
    /// Player's Score
    /// </summary>
    public int score;

    public EntitySave player;

    /// <summary>
    /// List of enemies
    /// </summary>
    public List<EntitySave> enemies;


}