using System;
using UnityEngine;

[Serializable]
public class EnemySave : EntitySave 
{
    /// <summary>
    ///  ID of the enemy
    /// </summary>
    [SerializeField]
    public int ID;
}