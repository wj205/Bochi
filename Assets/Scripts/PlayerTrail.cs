using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTrail : MonoBehaviour {

	Transform _playerTransform;
	public GameObject trailPrefab;
	List<Vector3> trailObjectPositions;
	List<Quaternion> trailObjectRotations;
	public List<GameObject> trailObjects;

	void Start()
	{
		_playerTransform = GameObject.FindObjectOfType<Arrow>().transform;
		trailObjectPositions = new List<Vector3>();
		trailObjectRotations = new List<Quaternion>();
		trailObjects = new List<GameObject>();
	}

	void Update()
	{
		HandleTrail();
	}

	float timer = 0;
	void HandleTrail()
	{
		if(timer < 0.2f)
		{
			timer += Time.deltaTime;
		}
		else
		{
			trailObjectPositions.Add (_playerTransform.position);
			trailObjectRotations.Add (_playerTransform.rotation);
			timer = 0f;
		}
	}

	public void DrawTrail()
	{
		for(int i = 0; i < trailObjectPositions.Count; i++)
		{
			GameObject trailObject = Instantiate (trailPrefab, trailObjectPositions[i], trailObjectRotations[i]) as GameObject;
			trailObjects.Add (trailObject);
		}
	}

	public void EraseTrail()
	{
		Debug.Log ("number of positions: " + trailObjectPositions.Count.ToString ());
		Debug.Log ("number of rotations: " + trailObjectRotations.Count.ToString ());
		Debug.Log ("number of objects: " + trailObjects.Count.ToString ());
		for(int i = 0; i < trailObjects.Count; i++)
		{
			Destroy (trailObjects[i]);
		}
		trailObjectPositions.Clear ();
		trailObjectRotations.Clear ();
		trailObjects.Clear ();
	}
}
