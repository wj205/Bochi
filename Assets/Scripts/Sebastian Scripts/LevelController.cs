using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    public LevelState state;

    //fader vars
    public float fadeSpeed = 5f;
    private Fader[] _faders;

    public Color levelColor;
    
    //target vars
    private Target[] _targets;

    private PlayerController _player;

	// Use this for initialization
	void Start () {
        this._player = GameObject.FindObjectOfType<PlayerController>();
        this._faders = GameObject.FindObjectsOfType<Fader>();
        this._targets = GameObject.FindObjectsOfType<Target>();
        this.SwitchToState(LevelState.LOADIN);
	}
	
	// Update is called once per frame
	void Update () {
        HandleStates();
	}

    void HandleStates()
    {
        switch (this.state)
        {
            case LevelState.LOADIN:
                this.OnLoadIn();
                break;
            case LevelState.IDLE:
                this.OnIdle();
                break;
            case LevelState.WAITING:
                this.OnWaiting();
                break;
            case LevelState.PAUSED:
                this.OnPaused();
                break;
            case LevelState.LOADOUT:
                this.OnLoadOut();
                break;
        }
    }


    void OnLoadIn() 
    {
        //if (this.CheckForCompletedFade())
        //{
            this.SwitchToState(LevelState.IDLE);
        //}
    }

    void OnIdle() { }
    void OnWaiting() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.ResetLevel();
        }    
    }
    void OnPaused() { }

    void OnLoadOut() 
    { 
        if (this.CheckForCompletedFade()) 
        {
            this.LoadNextLevel();
        }
    }

    public void SwitchToState(LevelState s)
    {
        this.state = s;
        Debug.Log("LevelController is switching to state: " + this.state);
        switch (this.state)
        {
            case LevelState.LOADIN:
                this.SwitchToLoadIn();
                break;
            case LevelState.IDLE:
                this.SwitchToIdle();
                break;
            case LevelState.WAITING:
                this.SwitchToWaiting();
                break;
            case LevelState.PAUSED:
                this.SwitchToPaused();
                break;
            case LevelState.LOADOUT:
                this.SwitchToLoadOut();
                break;
        }
    }

    void SwitchToLoadIn() 
    {
        for (int i = 0; i < _faders.Length; i++)
        {
            this._faders[i].fadeSpeed = this.fadeSpeed;
            if (!this._faders[i].tag.Equals("Player"))
            {
                this._faders[i].SwitchToState(FadeState.IN);
            }
        }
    }

    void SwitchToIdle() 
    {
    }
    
    void SwitchToWaiting() 
    {
    }
    void SwitchToPaused() { }
    
    void SwitchToLoadOut() 
    {
        for (int i = 0; i < _faders.Length; i++)
        {
            this._faders[i].SwitchToState(FadeState.OUT);
        }
    }


    bool CheckForCompletedFade()
    {
        for (int i = 0; i < _faders.Length; i++)
        {
            if (!_faders[i].state.Equals(FadeState.IDLE))
            {
                return false;
            }
        }
        return true;
    }


    public void CheckForWinCondition()
    {
        bool won = true;

        for (int i = 0; i < _targets.Length; i++)
        {
            if (_targets[i].state.Equals(TargetState.UNHIT))
            {
                won = false;
                break;
            }
        }

        if (won)
        {
            this.SwitchToState(LevelState.LOADOUT);
        }
    }


    void LoadNextLevel()
    {
        Debug.Log("Loading next level");
        Application.LoadLevel((Application.loadedLevel + 1) % Application.levelCount);
    }

    void LoadStartMenu()
    {
        Application.LoadLevel(0);
    }

    public void ResetLevel()
    {
        this._player.SwitchToState(PlayerState.IDLE);

        for (int i = 0; i < _targets.Length; i++)
        {
            this._targets[i].SwitchToState(TargetState.UNHIT);
        }

        this.SwitchToState(LevelState.LOADIN);
    }
}

public enum LevelState
{
    LOADIN, IDLE, WAITING, PAUSED, LOADOUT
}
