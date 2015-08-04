using UnityEngine;
using System.Collections;

public class MovingObstacle : MonoBehaviour {

	public Vector3 position1;
	public Vector3 position2;

	public float moveSpeed = 1;

	float timer = 0;
	bool atPos1 = false;
	bool atPos2 = false;

	void Update () {

		/*if(timer < moveSpeed / 2f)
		{
			transform.position = Vector3.Lerp (this.transform.position, position2, Time.deltaTime);
			timer += Time.deltaTime;
		}else if(timer >= moveSpeed / 2f && timer < moveSpeed)
		{
			transform.position = Vector3.Lerp (this.transform.position, position1, Time.deltaTime);
			timer += Time.deltaTime;
		}else
		{
			timer = 0;
		}*/

		if(isPositionEqual(this.transform.position, position1))
		{
			Debug.Log ("at pos 1");
			atPos2 = false;
			atPos1 = true;
		}else if(isPositionEqual(this.transform.position, position2))
		{
			Debug.Log ("at pos 2");
			atPos1 = false;
			atPos2 = true;
		}

		if(atPos1)
		{
			transform.position = Vector3.MoveTowards (this.transform.position, position2, Time.deltaTime * moveSpeed);
		}else if(atPos2)
		{
			transform.position = Vector3.MoveTowards (this.transform.position, position1, Time.deltaTime * moveSpeed);
		}else
		{
			transform.position = Vector3.MoveTowards (this.transform.position, position1, Time.deltaTime * moveSpeed);
		}
	}

	bool isPositionEqual(Vector3 pos1, Vector3 pos2)
	{
		if(Mathf.Abs (pos1.x - pos2.x) < 0.01 && Mathf.Abs (pos1.y - pos2.y) < 0.01)
		{
			return true;
		}else
		{
			return false;
		}
	}
}
