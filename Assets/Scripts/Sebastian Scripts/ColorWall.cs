using UnityEngine;
using System.Collections;

public class ColorWall : MonoBehaviour {

    Renderer _renderer;
    Fader _fader;
    public Color color;
	// Use this for initialization
    void Start()
    {
        _renderer = this.GetComponent<Renderer>();
        _fader = this.GetComponent<Fader>();
        _fader.SetColor(this.color);
        _renderer.material.color = color;
        color.a = 255f;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player") && !Fader.isColorEqual(other.gameObject.GetComponent<PlayerController>().interactableColor,this.color))
        {
            PlayerController p = other.gameObject.GetComponent<PlayerController>();

            p.interactableColor = this.color;
            Debug.Log(p.interactableColor.ToHexStringRGB());
            p.PushNewTrail(this.color);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player") && !Fader.isColorEqual(other.gameObject.GetComponent<PlayerController>().interactableColor, this.color))
        {
            PlayerController p = other.GetComponent<PlayerController>();

			p.interactableColor = this.color;
			Debug.Log (p.interactableColor.ToHexStringRGB ());
            p.PushNewTrail(this.color);
        }
    }
}
