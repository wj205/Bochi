using UnityEngine;
using System.Collections;

public class MovingObstacle : MonoBehaviour {
	
	public float moveSpeed = 1;

	/*public Vector3 position1;
	public Vector3 position2;

	bool atPos1 = false;
	bool atPos2 = false;*/

	public Vector3[] targetPositions;
	bool[] atPos;

	void Start()
	{
		atPos = new bool[targetPositions.Length];
	}

	void Update () {
		for(int i = 0; i < targetPositions.Length; i++)
		{
			if(isPositionEqual (this.transform.position, targetPositions[i]))
			{
				for(int x = 0; x < atPos.Length; x++)
				{
					atPos[x] = false;
				}
				atPos[i] = true;
			}
		}
		if(isAtPos ())
		{
			MoveObject();
		}else
		{
			transform.position = Vector3.MoveTowards (this.transform.position, targetPositions[0], Time.deltaTime * moveSpeed);
		}
	}

	void MoveObject()
	{
		for(int x = 0; x < atPos.Length; x++)
		{
			if(atPos[x])
			{
				if(x == atPos.Length - 1)
				{
					transform.position = Vector3.MoveTowards (this.transform.position, targetPositions[0], Time.deltaTime * moveSpeed);
				}else
				{
					transform.position = Vector3.MoveTowards (this.transform.position, targetPositions[x + 1], Time.deltaTime * moveSpeed);
				}
			}
		}
	}

	bool isAtPos()
	{
		for(int i = 0; i < atPos.Length; i++)
		{
			if(atPos[i])
			{
				return true;
			}
		}
		return false;
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
