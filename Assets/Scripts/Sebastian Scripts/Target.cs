using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(Rotater))]
public class Target : MonoBehaviour {

    private LevelController _levelController;
    private Fader _fader;
    private Collider2D _collider;
	private Renderer _renderer;
    private Rotater _rotater;
    private Quaternion _originalRotation;
    private ColorFlash _flash;
    public TargetState state = TargetState.UNHIT;

    public float missileSpeed = 5f;
    private Missile[] _missiles;

	// Use this for initialization
	void Start () {
        this._levelController = GameObject.FindObjectOfType<LevelController>();
        this._flash = GameObject.FindObjectOfType<ColorFlash>();
        this._fader = this.GetComponent<Fader>();
        this._collider = this.GetComponent<Collider2D>();
		this._renderer = this.GetComponent<Renderer>();
        this.state = TargetState.UNHIT;
        this._rotater = GetComponent<Rotater>();
		if (!this._rotater) this._rotater = this.gameObject.AddComponent<Rotater>(); //testing
        this._originalRotation = this.transform.rotation;
        _missiles = this.GetComponentsInChildren<Missile>();
	}


    public void SwitchToState(TargetState s)
    {
        this.state = s;
        switch (this.state)
        {
            case TargetState.UNHIT:
                this.SwitchToUnhit();
                break;
            case TargetState.HIT:
                this.SwitchToHit();
                break;
        }
    }

    protected virtual void SwitchToUnhit()
    {
        this.transform.rotation = _originalRotation;
        this._rotater.enabled = true;
        for (int i = 0; i < _missiles.Length; i++)
        {
            _missiles[i].Reset();
        }

        this._fader.SwitchToState(FadeState.IN);
        this._collider.enabled = true;
    }

    protected virtual void SwitchToHit()
    {
        this._rotater.enabled = false;
        this._fader.SwitchToState(FadeState.OUT);
        this._collider.enabled = false; 
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player") || other.tag.Equals("Missile"))
        {
			PlayerController p = other.GetComponent<PlayerController>();
			if(this.GetComponent<ColorTarget>())
			{
				if(isColorEqual(_renderer.material.color, p.interactableColor))
				{
                    this._flash.flashColor(_renderer.material.color);
                    this.FireMissiles();
					this.SwitchToState(TargetState.HIT);
					_levelController.CheckForWinCondition();           
				}
			}else
            {
                this._flash.flashColor(_renderer.material.color);
                this.FireMissiles();
            	this.SwitchToState(TargetState.HIT);
            	_levelController.CheckForWinCondition();
			}
        }
    }

	bool isColorEqual(Color c1, Color c2)
	{
		float t = 0.01f;

		//NOTE (WILL): RIGHT NOW, CHECKING FOR ALPHA TO BE THE SAME ISN'T WORKING. PROBABLY JUST SOMETHING STUPID I'M FORGETTING,
		//ALTHOUGH I DON'T KNOW IF IT'S NECESSARY
		if (Mathf.Abs(c1.r - c2.r) < t && Mathf.Abs(c1.g - c2.g) < t && Mathf.Abs(c1.b - c2.b) < t)
		{
			Debug.Log ("color equal");
			return true;
		}
		else
		{
			Debug.Log ("color not equal");
			return false;
		}
	}

    void FireMissiles()
    {
        for (int i = 0; i < _missiles.Length; i++)
        {
            _missiles[i].Fire(this.transform.position, missileSpeed);
        }
    }
}

public enum TargetState
{
    UNHIT, HIT
}