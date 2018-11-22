using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InputField))]
public class ConsoleInputField : MonoBehaviour {

	public Console TargetConsole;

	private InputField _input;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		_input = GetComponent<InputField>();
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		// _input.Select();
		// _input.ActivateInputField();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		// if (!_input.isFocused)
		// {
		// 	try
		// 	{
		// 		_input.ActivateInputField();
		// 	}
		// 	catch (Exception e)
		// 	{
		// 		ExceptionText.text = e.Message;
		// 	}
		// }

		// focued, not empty and pressed enter/return
		if (_input.text != "" && Input.GetKey(KeyCode.Return))
		{
			TargetConsole.ProcessCommand(_input.text);
			
			_input.text = "";
		}
	}
}
