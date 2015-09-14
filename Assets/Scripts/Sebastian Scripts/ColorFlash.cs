using UnityEngine;
using System.Collections;

public class ColorFlash : MonoBehaviour {

    Renderer _renderer;
    Camera cam;
    public float flashSpeed = 1f;
    private Color _originalColor;
    private Color _goalColor;
    private bool flashing = false;

    void Start()
    {
        this._renderer = GetComponent<Renderer>();
        cam = Camera.main;
        this._originalColor = this._renderer.material.color;
    }

    void Update()
    {
        if (flashing)
        {
            this.cam.backgroundColor = Color.Lerp(this.cam.backgroundColor, _goalColor, Time.deltaTime * flashSpeed);

            if (Fader.isColorEqual(this.cam.backgroundColor,_goalColor)) 
            {
                flashing = false;
            }
        }
        else
        {
            this.cam.backgroundColor = Color.Lerp(this.cam.backgroundColor, _originalColor, Time.deltaTime * flashSpeed);
        }
    }

    public void Flash(Color c)
    {
        c.a = c.a / 2f;
        c.r /= 2f;
        c.g /= 2f;
        c.b /= 2f;
        this._goalColor = c;
        this.flashing = true;
    }
}
