using UnityEngine;
using System.Collections;

public class WorldPanels : MonoBehaviour {

	public int firstLevelInteger;
	GameObject[] _children;

	void Start()
	{
		int furthestLevel = PlayerPrefs.GetInt ("FurthestLevel", 0);
		if(furthestLevel < firstLevelInteger)
		{
			this.gameObject.SetActive (false);
		}
		_children = new GameObject[this.transform.childCount];
		for(int i = 0; i < this.transform.childCount; i++)
		{
			_children[i] = transform.GetChild (i).gameObject;
			//transform.GetChild (i).GetComponent<LevelButtons>().levelBuildInteger = firstLevel + i;
		}

		for (int i = 0; i < _children.Length; i++)
		{
			if(_children[i].GetComponent<LevelButtons>() != null)
			{
				int levelNum = firstLevelInteger + i - 1;
				LevelButtons levelButton = _children[i].GetComponent<LevelButtons>();
				if(levelNum > furthestLevel)
				{
					levelButton.SetLevelLocked ();
				}
				levelButton.levelBuildInteger = levelNum;
				levelButton.SetText (levelNum + 1);
			}
		}
	}
}
