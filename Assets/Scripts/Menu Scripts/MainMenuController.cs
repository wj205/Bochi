using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	//Save the last level the player has played in an int
	int startLevel;

	void Start()
	{
		startLevel = PlayerPrefs.GetInt ("CurrentLevel", 0); //Default to the first level
	}

	public void StartGame()
	{
		Application.LoadLevel(startLevel);
	}

	public void GoToLevelSelect()
	{
		Application.LoadLevel ("LevelSelect");
	}

	public void GoToOptions()
	{
		Application.LoadLevel ("Options");
	}
}
