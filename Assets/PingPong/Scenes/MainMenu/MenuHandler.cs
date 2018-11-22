using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

	public Button HumanBtn;

	public Button AiBtn;

	public GameObject Selector;

	public int IndexMenu;
	
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		#region ps4 exclusive controls
		#if UNITY_PS4

		float v = Input.GetAxis("P1VertJoy");

		if (v > 0)
		{
			IndexMenu = 0;
			Selector.transform.SetParent(HumanBtn.gameObject.transform, false);
			
		}
		else if (v < 0)
		{
			IndexMenu = 1;
			Selector.transform.SetParent(AiBtn.gameObject.transform, false);
		}

		float submit = Input.GetAxis("P1Submit");

		if (submit != 0)
		{
			if (IndexMenu == 0)
			{
				HumanBtn.onClick.Invoke();
			}
			else if (IndexMenu == 1)
			{
				AiBtn.onClick.Invoke();
			}
		}

		#endif
		#endregion
	}
}
