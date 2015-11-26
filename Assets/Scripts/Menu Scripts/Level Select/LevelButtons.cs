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

	void SetLevelLocked()
	{
		locked = true;
		_button.interactable = false;
	}

	public void LoadLevel()
	{
		Application.LoadLevel (levelBuildInteger);
	}
}
