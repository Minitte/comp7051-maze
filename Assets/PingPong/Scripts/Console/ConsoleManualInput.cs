using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManualInput : MonoBehaviour {

	public InputField ConsoleInputField;

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		PostKeyToConsole(KeyCode.A);
		PostKeyToConsole(KeyCode.B);
		PostKeyToConsole(KeyCode.C);
		PostKeyToConsole(KeyCode.D);
		PostKeyToConsole(KeyCode.E);
		PostKeyToConsole(KeyCode.F);
		PostKeyToConsole(KeyCode.G);
		PostKeyToConsole(KeyCode.H);
		PostKeyToConsole(KeyCode.I);
		PostKeyToConsole(KeyCode.J);
		PostKeyToConsole(KeyCode.K);
		PostKeyToConsole(KeyCode.L);
		PostKeyToConsole(KeyCode.M);
		PostKeyToConsole(KeyCode.N);
		PostKeyToConsole(KeyCode.O);
		PostKeyToConsole(KeyCode.P);
		PostKeyToConsole(KeyCode.Q);
		PostKeyToConsole(KeyCode.R);
		PostKeyToConsole(KeyCode.S);
		PostKeyToConsole(KeyCode.T);
		PostKeyToConsole(KeyCode.U);
		PostKeyToConsole(KeyCode.V);
		PostKeyToConsole(KeyCode.W);
		PostKeyToConsole(KeyCode.X);
		PostKeyToConsole(KeyCode.Y);
		PostKeyToConsole(KeyCode.Z);

		#region Alpha Numbers
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			ConsoleInputField.text += "0";
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			ConsoleInputField.text += "1";
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			ConsoleInputField.text += "2";
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			ConsoleInputField.text += "3";
		}

		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			ConsoleInputField.text += "4";
		}

		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			ConsoleInputField.text += "5";
		}

		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			ConsoleInputField.text += "6";
		}

		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			ConsoleInputField.text += "7";
		}

		if (Input.GetKeyDown(KeyCode.Alpha8))
		{
			ConsoleInputField.text += "8";
		}

		if (Input.GetKeyDown(KeyCode.Alpha9))
		{
			ConsoleInputField.text += "9";
		}
		#endregion

		if (Input.GetKeyDown(KeyCode.Space))
		{
			ConsoleInputField.text += " ";
		}
		

		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			string console = ConsoleInputField.text;

			if (ConsoleInputField.text.Length > 0)
			{
				ConsoleInputField.text = console.Remove(console.Length - 1, 1);;
			}
		}

	}

	public void PostKeyToConsole(KeyCode key)
	{
		if (Input.GetKeyDown(key))
		{
			ConsoleInputField.text += key.ToString().ToLower();
		}
	}
}
