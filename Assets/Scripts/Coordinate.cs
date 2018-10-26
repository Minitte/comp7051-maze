using System;

/// <summary>
/// This is a class for 2D coordinates.
/// </summary>
[Serializable]
public class Coordinate {
    public static Coordinate north {
        get {
            return new Coordinate(0, 1);
        }
    }

    public static Coordinate east {
        get {
            return new Coordinate(1, 0);
        }
    }

    public static Coordinate south {
        get {
            return new Coordinate(0, -1);
        }
    }

    public static Coordinate west {
        get {
            return new Coordinate(-1, 0);
        }
    }

    /// <summary>
    /// The x coordinate.
    /// </summary>
    public int x;

    /// <summary>
    /// The z coordinate.
    /// </summary>
    public int z;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="x">The x coordinate</param>
    /// <param name="z">the z coordinate</param>
    public Coordinate(int x, int z) {
        this.x = x;
        this.z = z;
    }

    /// <summary>
    /// Gives a coordinate to traverse through given an input direction.
    /// </summary>
    /// <param name="dir">The direction to go as an int</param>
    /// <returns>The direction to go as a coordinate</returns>
    public static Coordinate GetCoordinate(int dir) {
        switch (dir) {
        case 0:
            return north;
        case 1:
            return east;
        case 2:
            return south;
        case 3:
            return west;
        }

        return null;
    }

    /// <summary>
    /// Operator overload for + and +=.
    /// </summary>
    /// <param name="lhs">The left hand side of the assignment</param>
    /// <param name="rhs">The right hand side of the assignment</param>
    /// <returns></returns>
    public static Coordinate operator+(Coordinate lhs, Coordinate rhs) {
        return new Coordinate(lhs.x + rhs.x, lhs.z + rhs.z);
    }
}