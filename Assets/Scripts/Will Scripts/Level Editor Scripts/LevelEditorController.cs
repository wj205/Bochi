using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class LevelEditorController : MonoBehaviour {

	public bool editorMode = true;
	public bool playMode;

	public GameObject targetPrefab;
	public GameObject movingObstaclePrefab;
	public GameObject portalPrefab;

	GameObject currentObj;

	LevelController _levelController;
	PlayerController _player;

	// Use this for initialization
	void Start () {
		_levelController = GameObject.FindObjectOfType<LevelController>().GetComponent<LevelController>();
		_levelController.enabled = false;
		_player = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
		_player.gameObject.SetActive (false);
	}

	void Update()
	{
		if(EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton (0))
		{
			currentObj.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Debug.Log (currentObj.transform.position);
		}
		if(Input.GetMouseButtonUp (0))
		{
			currentObj = null;
		}
	}
	
	public void ToggleObjectButtons()
	{

	}

	void SpawnObject(GameObject obj)
	{
		GameObject newMovingObj = Instantiate (obj, Camera.main.ScreenToWorldPoint (Input.mousePosition), Quaternion.identity) as GameObject;
		currentObj = newMovingObj;
	}

	public void SpawnTarget()
	{

	}

	public void SpawnMovingObstacle()
	{
		SpawnObject (movingObstaclePrefab);
	}

	public void SpawnPortal()
	{

	}

}
