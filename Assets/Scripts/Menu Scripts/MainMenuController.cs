using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	//Save the last level the player has played in an int
	int startLevel;

	void Start()
	{
		startLevel = PlayerPrefs.GetInt ("CurrentLevel", 1); //Default to the first level
	}

	public void StartGame()
	{
		Application.LoadLevel(startLevel);
	}
}
