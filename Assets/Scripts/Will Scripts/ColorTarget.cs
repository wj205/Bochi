using UnityEngine;
using System.Collections;

public class ColorTarget : MonoBehaviour {

	Renderer _renderer;
	public Color color;

	void Awake () 
	{
		_renderer = this.GetComponent<Renderer>();
		_renderer.material.color = color;
		//color.a = 255f;
	}
}
