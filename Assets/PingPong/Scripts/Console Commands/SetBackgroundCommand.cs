using System;
using UnityEngine;

public class SetBackgroundCommand : ConsoleCommand
{
    
    public SetBackgroundCommand()
    {
        _helpMessage = "background <colour>\nColours: Red, Blue, Green, Black, White";
    }

    public override string ProcessCommand(string[] args)
    {
        if (args.Length != 2)
        {
            return _helpMessage;
        }

        string colorString = args[1].ToLower();

        if (colorString.Equals("red", StringComparison.InvariantCultureIgnoreCase))
        {
            Camera.main.backgroundColor = Color.red;
        }
        else if (colorString.Equals("blue", StringComparison.InvariantCultureIgnoreCase))
        {
            Camera.main.backgroundColor = Color.blue;
        }
        else if (colorString.Equals("green", StringComparison.InvariantCultureIgnoreCase))
        {
            Camera.main.backgroundColor = Color.green;
        }
        else if (colorString.Equals("black", StringComparison.InvariantCultureIgnoreCase))
        {
            Camera.main.backgroundColor = Color.black;
        }
        else if (colorString.Equals("white", StringComparison.InvariantCultureIgnoreCase))
        {
            Camera.main.backgroundColor = Color.white;
        }
        else    // unknown colour
        {
            return _helpMessage;
        }

        return "Background color changed to " + colorString;
    }
}
