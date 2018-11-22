using UnityEngine;
using UnityEngine.UI;

public class SetScoreCommand : ConsoleCommand
{

    public SetScoreCommand()
    {
        _helpMessage = "SetScore <Player> <Score>";
    }

    public override string ProcessCommand(string[] args)
    {

        int player = -1;
        int score = 0;

        if (args.Length != 3 || !int.TryParse(args[1], out player) || !int.TryParse(args[2], out score))
        {
            return _helpMessage;
        }

        if (player != 1 && player != 2)
        {
            return _helpMessage;
        }

        GameObject scoreGO = GameObject.Find("Player " + player + " Score");

        scoreGO.GetComponent<PlayerScoreText>().Score = score;

        scoreGO.GetComponent<Text>().text = score + "pt";

        GameObject gmGO = GameObject.Find("Game Manager");

        gmGO.GetComponent<GameManager>().Scores[player - 1] = score;

        return "Player " + player + "'s score set to " + score;
    }
}