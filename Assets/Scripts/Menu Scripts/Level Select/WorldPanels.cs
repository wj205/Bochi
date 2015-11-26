using UnityEngine;
using System.Collections;

public class WorldPanels : MonoBehaviour {

	public int firstLevel;

	void Start()
	{
		if(PlayerPrefs.GetInt ("FurthestLevel", 0) < firstLevel)
		{
			this.gameObject.SetActive (false);
		}
	}
}
