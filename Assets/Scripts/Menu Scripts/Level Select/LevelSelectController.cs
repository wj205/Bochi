using UnityEngine;
using System.Collections;

public class LevelSelectController : MonoBehaviour {

	public bool scrollingEnabled = false;
	Canvas _canvas;
	GameObject[] _worlds;

	void Start()
	{
		int furthestLevel = PlayerPrefs.GetInt ("FurthestLevel", 0);
		if(furthestLevel >= 50)
		{
			//Horizontal scrolling enabled
			scrollingEnabled = true;
		}
		_canvas = GameObject.FindObjectOfType<Canvas>();
		_worlds = new GameObject[_canvas.transform.childCount];
		for(int i = 0; i < _canvas.transform.childCount; i++)
		{
			_worlds[i] = _canvas.transform.GetChild (i).gameObject;
		}
	}

	void Update () {
		if(Input.GetKeyDown (KeyCode.Escape))
		{
			Application.LoadLevel ("menu_main");
		}

		if(scrollingEnabled)
		{
			HandleScrolling();
		}
	}

	float amountScrolled;
	void HandleScrolling()
	{
		float mousePositionX = Camera.main.ScreenToViewportPoint (Input.mousePosition).x;

		if(mousePositionX > 0.9f)
		{
			float distance = 1f - mousePositionX;
			for(int i = 0; i < _worlds.Length; i++)
			{
				_worlds[i].transform.position = new Vector3(_worlds[i].transform.position.x - 5f, _worlds[i].transform.position.y, _worlds[i].transform.position.z);
				amountScrolled += 1f;
			}
		}else if(mousePositionX < 0.1f && amountScrolled > 0f)
		{
			for(int i = 0; i < _worlds.Length; i++)
			{
				_worlds[i].transform.position = new Vector3(_worlds[i].transform.position.x + 5f, _worlds[i].transform.position.y, _worlds[i].transform.position.z);
				amountScrolled -= 1f;
			}
		}

		/*if(Input.GetMouseButtonDown (0))
		{
			initialMousePosx = Input.mousePosition.x;
		}
		if(Input.GetMouseButton(0))
		{
			if(timer < 0.2f)
			{
				timer += Time.deltaTime;
			}else
			{
				float currentMouseposx = Input.mousePosition.x;
				float mouseDiff = initialMousePosx - currentMouseposx;
				for(int i = 0; i < _worlds.Length; i++)
				{
					_worlds[i].transform.position = new Vector3(_worlds[i].transform.position.x - mouseDiff/100f, _worlds[i].transform.position.y, _worlds[i].transform.position.z);
				}
			}
		}

		if(Input.GetMouseButtonUp (0))
		{
			timer = 0f;
		}*/
	}
}
