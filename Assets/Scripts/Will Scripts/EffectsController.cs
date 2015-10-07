using UnityEngine;
using System.Collections;

public class EffectsController : MonoBehaviour {

	Light[] _lights;
	BackgroundPlane _backgroundPlane;

	float duration;

	void Start()
	{
		_lights = GameObject.FindObjectsOfType<Light>();
		_backgroundPlane = GameObject.FindObjectOfType<BackgroundPlane>().GetComponent<BackgroundPlane>();
		duration = _backgroundPlane.decayRate;
		foreach(Light light in _lights)
		{
			light.gameObject.SetActive (false);
		}
		_backgroundPlane.gameObject.SetActive (false);
	}

	public void TurnOnBackgroundEffects()
	{
		StartCoroutine("BGEffects");
	}

	IEnumerator BGEffects()
	{
		foreach(Light light in _lights)
		{
			light.gameObject.SetActive (true);
		}
		_backgroundPlane.gameObject.SetActive (true);
		float timer = 0f;
		while(timer < duration)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		foreach(Light light in _lights)
		{
			light.gameObject.SetActive (false);
		}
		_backgroundPlane.gameObject.SetActive (false);
	}
}
