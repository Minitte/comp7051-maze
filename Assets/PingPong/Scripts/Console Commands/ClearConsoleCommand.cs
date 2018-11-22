using UnityEngine;
using UnityEngine.UI;

public class ClearConsoleCommand : ConsoleCommand
{

    public ClearConsoleCommand()
    {
        _helpMessage = "clear";
    }

    public override string ProcessCommand(string[] args)
    {
        if (args.Length != 1)
        {
            return _helpMessage;
        }

        GameObject consoleGO = GameObject.Find("Console");

        consoleGO.GetComponent<Console>().ConsoleOutput.text = "";

        return "";
    }



}