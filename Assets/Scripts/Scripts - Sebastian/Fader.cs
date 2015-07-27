using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class Fader : MonoBehaviour {

    private Renderer _renderer;
    private Material _material;

    public FadeState state;

    public float fadeSpeed = 5f;
    private Color _originalColor;
    private Color _originalColorZeroAlpha;

	// Use this for initialization
	void Start () {
        this._renderer = this.GetComponent<Renderer>();
        this._material = this._renderer.material;
        this._originalColor = this._material.color;
        Color c = this._material.color;
        c.a = 0;
        _originalColorZeroAlpha = c;
        this._material.color = c;
	}
	
	// Update is called once per frame
	void Update () {
        HandleStates();
	}

    void HandleStates()
    {
        switch (this.state)
        {
            case FadeState.IN:
                this.OnFadeIn();
                break;
            case FadeState.IDLE:
                this.OnIdle();
                break;
            case FadeState.OUT:
                this.OnFadeOut();
                break;
        }
    }

    void OnFadeIn()
    {
        this._material.color = Color.Lerp(this._material.color, this._originalColor, Time.deltaTime * this.fadeSpeed);

        if (Fader.isColorEqual(this._material.color,this._originalColor))
        {
            this.SwitchToState(FadeState.IDLE);
        }
    }

    void OnIdle() { }

    void OnFadeOut()
    {
        this._material.color = Color.Lerp(this._material.color, this._originalColorZeroAlpha, Time.deltaTime * this.fadeSpeed);

        if (Fader.isColorEqual(this._material.color,this._originalColorZeroAlpha))
        {
            this.SwitchToState(FadeState.IDLE);
        }
    }

    public void SwitchToState(FadeState s)
    {
        this.state = s;
        switch (this.state)
        {
            case FadeState.IN:
                this.SwitchToFadeIn();
                break;
            case FadeState.IDLE:
                this.SwitchToIdle();
                break;
            case FadeState.OUT:
                this.SwitchToFadeOut();
                break;
        }
    }

    void SwitchToFadeIn() { }
    void SwitchToIdle() { }
    void SwitchToFadeOut() { }

    public void SetColor(Color c)
    {
        this._material.color = c;
    }

    public bool isVisible()
    {
        return (this._material.color.a < 1f ? false : true);
    }

    static bool isColorEqual(Color c1, Color c2)
    {
        float t = 0.01f;

        if (Mathf.Abs(c1.r - c2.r) < t && Mathf.Abs(c1.g - c2.g) < t && Mathf.Abs(c1.b - c2.b) < t && Mathf.Abs(c1.a - c2.a) < t)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public enum FadeState
{
    IN, IDLE, OUT
}
