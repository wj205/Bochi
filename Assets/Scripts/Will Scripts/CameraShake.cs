using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public float shakeLength = 1f;
	Vector3 baseCameraPosition;

	void Start()
	{
		baseCameraPosition = this.transform.position;
	}

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.Space))
		{
			ScreenShake ();
		}
	}

	public void ScreenShake()
	{
		//shakeLength = 1f;
		StartCoroutine ("ScreenShakeCoroutine");
	}

	IEnumerator ScreenShakeCoroutine()
	{
		float t = shakeLength;
		while(t > 0f)
		{
			this.transform.position = baseCameraPosition + t * new Vector3(
				Mathf.Sin (Time.time * Random.Range (2.5f, 5f)),
				Mathf.Sin (Time.time * Random.Range (2.5f, 5f)),
				0f);
			t -= Time.deltaTime;
			yield return 0;
		}
		yield return null;
	}
}
