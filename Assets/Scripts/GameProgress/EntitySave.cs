
using System;
using UnityEngine;

[Serializable]
public class EntitySave 
{

    /// <summary>
    /// Entity's position
    /// </summary>
    /// <value></value>
    public Vector3 position 
    {
        get 
        {
            return new Vector3(rotX, rotY, rotZ);
        }

        set
        {
            rotX = value.x;
            rotY = value.y;
            rotZ = value.z;
        }
    }

    /// <summary>
    /// Dead flag
    /// </summary>
    public bool dead;

    /// <summary>
    /// Position of entity
    /// </summary>
    private float rotX, rotY, rotZ;
}