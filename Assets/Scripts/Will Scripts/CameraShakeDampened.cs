using UnityEngine;
using System.Collections;

public class CameraShakeDampened : MonoBehaviour {

	public float duration = 0.15f;
	public float magnitude = 0.1f;

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.Space))
		{
			StartCoroutine (Shake ());
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		BackgroundPlane.value = 1f;
		StartCoroutine(Shake ());
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		BackgroundPlane.value = 1f;
		StartCoroutine(Shake ());
	}

	IEnumerator Shake()
	{
		float elapsed = 0f;

		Vector3 originalCamPos = Camera.main.transform.position;

		while(elapsed < duration)
		{

			elapsed += Time.deltaTime;

			float percentComplete = elapsed / duration;
			float damper = 1f - Mathf.Clamp (4f * percentComplete - 3f, 0f, 1f);

			float x = Random.value * 2f - 1f;
			float y = Random.value * 2f - 1f; 
			x *= magnitude * damper;
			y *= magnitude * damper;

			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

			yield return null;
		}

		Camera.main.transform.position = originalCamPos;
	}
}
