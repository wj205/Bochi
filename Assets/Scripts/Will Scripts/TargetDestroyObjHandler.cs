using UnityEngine;
using System.Collections;

public class TargetDestroyObjHandler : MonoBehaviour {

	public GameObject destroyObjPrefab;
	int objCount;
	int x = 0;
	public Color objColor;

	void Start()
	{
		//Call coroutine to spawn rand num of objs
		objCount = Random.Range (10,21);
		StartCoroutine ("SpawnDestroyObjects");
	}

	IEnumerator SpawnDestroyObjects()
	{
		while(x < objCount)
		{
			GameObject newDestroyObj = Instantiate (destroyObjPrefab, this.transform.position, Quaternion.identity) as GameObject;
			newDestroyObj.GetComponent<Renderer>().material.color = objColor;
			//newDestroyObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range (0f,1f), Random.Range (0f,1f)), ForceMode2D.Impulse);
			if(newDestroyObj.GetComponent<TargetDestroyObjects>() != null)
			{
				TargetDestroyObjects obj = newDestroyObj.GetComponent<TargetDestroyObjects>();
			}
			x++;
			yield return new WaitForSeconds(0.01f);
		}

		yield return null;
	}
}
