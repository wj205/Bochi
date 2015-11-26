using UnityEngine;
using System.Collections;

public class OptionsController : MonoBehaviour {

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.Escape))
		{
			LoadMainMenu();
		}
	}

	public void EraseSaveData()
	{
		PlayerPrefs.DeleteKey ("CurrentLevel");
		PlayerPrefs.DeleteKey ("FurthestLevel");
	}

	void LoadMainMenu()
	{
		Application.LoadLevel ("menu_main");
	}
}
