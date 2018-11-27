using System;
using UnityEngine;

public class GameProgressEncoder {

    /// <summary>
    /// Blocked constructor
    /// </summary>
    private GameProgressEncoder() {}

    /// <summary>
    /// Encodes the save file
    /// </summary>
    /// <param name="save">the save file to encode</param>
    /// <returns>A string containing the encoded save file</returns>
    public static string Encode(GameProgress save) {
        if (save == null) {
            Debug.Log("GameProgress given was null?");

            return null;
        }
        return JsonUtility.ToJson(save, true);
    }

    /// <summary>
    /// Decodes a string containing the encoded progress
    /// </summary>
    /// <param name="encoded">The encoded save file</param>
    /// <returns>A GameProgress object obtained from decoding the encoded string</returns>
    public static GameProgress Decode(string encoded) {
        GameProgress progress = null;

        try {
            progress = JsonUtility.FromJson<GameProgress>(encoded);
        } catch (ArgumentException aex) {
            Debug.Log("Failed to decode save!\n" + aex.Message);
        }

        return progress;
    }
}