using UnityEngine;
using System.Collections;

public class ColorWall : MonoBehaviour {

    Renderer _renderer;
    public Color color;
	// Use this for initialization
	void Start () {
        _renderer = this.GetComponent<Renderer>();
        _renderer.material.color = color;
        color.a = 255f;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerController p = other.GetComponent<PlayerController>();

			p.interactableColor = this.color;
			Debug.Log (p.interactableColor.ToHexStringRGB ());
            p.PushNewTrail(this.color);
        }
    }
}
