using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    private LevelController _levelController;
    private Fader _fader;
    private Collider2D _collider;

    public TargetState state = TargetState.UNHIT;

	// Use this for initialization
	void Start () {
        this._levelController = GameObject.FindObjectOfType<LevelController>();
        this._fader = this.GetComponent<Fader>();
        this._collider = this.GetComponent<Collider2D>();
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
            this.SwitchToState(TargetState.HIT);
            _levelController.CheckForWinCondition();
        }
    }
}

public enum TargetState
{
    UNHIT, HIT
}