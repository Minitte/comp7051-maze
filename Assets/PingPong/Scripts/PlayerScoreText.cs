using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerScoreText : MonoBehaviour {

	public int Score;

	public int OwningSlot;

	private Text _text;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		_text = gameObject.GetComponent<Text>();

		_text.text = Score + "pt";

		GameEventManager.OnGoal += IncScore;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="scoring"></param>
	public void IncScore(int scoring)
	{
		if (scoring == OwningSlot)
		{
			Score++;

			_text.text = Score + "pt";
		}
	}

}
