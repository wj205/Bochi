using UnityEngine;
using System.Collections;

public class ColorFlash : MonoBehaviour {

    public float flashSpeed = 5f;

    private bool _reached = false;

    Camera cam;
    Color _originalColor;
    Color _goalColor;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        _originalColor = cam.backgroundColor;
	}
	
	// Update is called once per frame
	void Update () {
        if (_reached)
        {
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, _originalColor, Time.deltaTime * flashSpeed);
        }
        else
        {
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, _goalColor, Time.deltaTime * flashSpeed);

            if (Fader.isColorEqual(cam.backgroundColor, _goalColor))
            {
                _reached = true;
            }
        }
	}

    public void flashColor(Color c)
    {
        _goalColor = c;
        _reached = false;
    }
}
