using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelButtons : MonoBehaviour, IPointerClickHandler {

	bool locked;
	public int levelBuildInteger;
	Button _button;

	void Start()
	{
		_button = this.GetComponent<Button>();
		//CheckIfLocked();
	}

	void CheckIfLocked()
	{
		if(PlayerPrefs.GetInt ("FurthestLevel", 0) < levelBuildInteger)
		{
			SetLevelLocked();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(!locked)
		{
			LoadLevel ();
		}
	}

	public void SetLevelLocked()
	{
		locked = true;
		this.GetComponent<Button>().interactable = false;
	}

	public void SetText(int levelNum)
	{
		this.GetComponentInChildren<Text>().text = "Level " + levelNum;
	}

	public void LoadLevel()
	{
		Application.LoadLevel (levelBuildInteger);
	}
}
