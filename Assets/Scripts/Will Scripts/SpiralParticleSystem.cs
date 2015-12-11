using UnityEngine;
using System.Collections;

public class SpiralParticleSystem : MonoBehaviour {

	public float rotationSpeed;
	Vector3 rotationVector;

	void Start()
	{
		rotationVector = new Vector3(0f,0f,rotationSpeed);
	}

	void Update () {
		this.transform.Rotate (rotationVector);
	}
}
