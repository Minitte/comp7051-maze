using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameVSBtn : MonoBehaviour {

	public void BeginGame()
    {
        SceneManager.LoadScene("Game");
    }
	
}
