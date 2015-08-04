using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    private LevelController _levelController;
    private Fader _fader;
    private Collider2D _collider;
	private Renderer _renderer;

    public TargetState state = TargetState.UNHIT;

	// Use this for initialization
	void Start () {
        this._levelController = GameObject.FindObjectOfType<LevelController>();
        this._fader = this.GetComponent<Fader>();
        this._collider = this.GetComponent<Collider2D>();
		this._renderer = this.GetComponent<Renderer>();
        this.state = TargetState.UNHIT;
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

        this._fader.SwitchToState(FadeState.IN);
        this._collider.enabled = true;
    }

    protected virtual void SwitchToHit()
    {
        this._fader.SwitchToState(FadeState.OUT);
        this._collider.enabled = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
			PlayerController p = other.GetComponent<PlayerController>();
			if(this.GetComponent<ColorTarget>())
			{
				if(isColorEqual(_renderer.material.color, p.interactableColor))
				{
					this.SwitchToState(TargetState.HIT);
					_levelController.CheckForWinCondition();
				}
			}else
			{
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
}

public enum TargetState
{
    UNHIT, HIT
}