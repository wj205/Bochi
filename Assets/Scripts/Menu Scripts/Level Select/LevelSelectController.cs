using UnityEngine;
using System.Collections;

public class LevelSelectController : MonoBehaviour {
	
	void Update () {
		if(Input.GetKeyDown (KeyCode.Escape))
		{
			Application.LoadLevel ("menu_main");
		}
	}
}
