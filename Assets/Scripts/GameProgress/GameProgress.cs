
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameProgress
{
    /// <summary>
    /// if the maze is old    
    /// </summary>
    [SerializeField]
    public bool oldMaze;

    /// <summary>
    /// Seed used for generating the maze
    /// </summary>
    [SerializeField]
    public string mazeSeed;

    /// <summary>
    /// Player's Score
    /// </summary>
    [SerializeField]
    public int score;

    /// <summary>
    /// Player position and info
    /// </summary>
    [SerializeField]
    public EntitySave player;

    /// <summary>
    /// List of enemies
    /// </summary>
    [SerializeField]
    public List<EnemySave> enemies;


}