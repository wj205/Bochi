using UnityEngine;
using System.Collections;

public class Asteroids : MonoBehaviour {

	bool movingRight;
	float targetY;

	float rotSpeedX;
	float rotSpeedY;
	float rotSpeedZ;
	Vector3 rotateSpeed;

	void Start()
	{
		if(this.transform.position.x < 0f)
		{
			//start out moving right
			movingRight = true;
		}else
		{
			//start out moving left
			movingRight = false;
		}
		rotSpeedX = Random.Range (0f, 2f);
		rotSpeedY = Random.Range (0f, 2f);
		rotSpeedZ = Random.Range (0f, 2f);
		rotateSpeed = new Vector3(rotSpeedX, rotSpeedY, rotSpeedZ);
		targetY = Random.Range (-5f, 5f);
	}

	void Update()
	{
		if(movingRight)
		{
			Vector3 targetPos = new Vector3(15f, targetY, 5f);
			this.transform.position = Vector3.MoveTowards (this.transform.position, targetPos, Time.deltaTime * Random.Range (2f, 5f));
		}else
		{
			Vector3 targetPos = new Vector3(-15f, targetY, 5f);
			this.transform.position = Vector3.MoveTowards (this.transform.position, targetPos, Time.deltaTime * Random.Range (2f, 5f));
		}

		this.transform.Rotate (rotateSpeed);

		if(this.transform.position.x > 16 || this.transform.position.x < -16)
		{
			Destroy (this.gameObject);
		}
	}
}
