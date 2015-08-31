using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Fader))]
public class PlayerController : MonoBehaviour {

    public bool staticStart = false;
    private Vector3 _startPosition;

    private LevelController _levelController;
    private Renderer _renderer;
    private Fader _fader;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private LineRenderer _lineRenderer;
    private PlayerTrail _playerTrail;
    public PlayerTrail playerTrailPrefab;

    private ColorFlash _flash;

    public float lineMaxDist = 5f;

    private Vector3 _mousePosition = Vector3.zero;
    private Vector3 _mousePosition2D = Vector3.zero;
    private bool _mouseover = false;

    public Vector3 startPoint;
    public float _gravity;
    public float shotmin;
    public float shotmax = 5f;

	public Color interactableColor;
    private List<PlayerTrail> _prevTrails = new List<PlayerTrail>();

    public PlayerState state;

	void Start () {
        _levelController = GameObject.FindObjectOfType<LevelController>();
        _renderer = this.GetComponent<Renderer>();
        _fader = this.GetComponent<Fader>();
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _collider = this.GetComponent<Collider2D>();
        _lineRenderer = this.GetComponent<LineRenderer>();
        _flash = GameObject.FindObjectOfType<ColorFlash>();
        _startPosition = this.transform.position;

		interactableColor = _levelController.levelColor;

        _playerTrail = Instantiate(playerTrailPrefab, this.transform.position, this.transform.rotation) as PlayerTrail;
        
        this.UpdateMousePosition();
        this.SwitchToState(PlayerState.IDLE);
	}

    void Update()
    {
        UpdateMousePosition();
        HandleStates();
		CheckVisible();
    }

    void UpdateMousePosition()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition2D = new Vector3(_mousePosition.x, _mousePosition.y, 0f);


        if (Physics2D.Raycast(_mousePosition, Vector2.zero))
        {
            _mouseover = true;
        }
        else
        {
            _mouseover = false;
        }
    }

    void HandleStates()
    {
        switch (this.state) 
        {
            case PlayerState.IDLE:
                this.OnIdle();
                break;
            case PlayerState.AIMING:
                this.OnAiming();
                break;
            case PlayerState.MOVING:
                this.OnMoving();
                break;
            case PlayerState.DISABLED:
                this.OnDisabled();
                break;
        }
    }

    void OnIdle() 
    {
        if (_levelController.state.Equals(LevelState.IDLE))
        {

            if (Input.GetMouseButtonDown(0) && !_mouseover)
            {
                this.SwitchToState(PlayerState.AIMING);
            }

        }
    }

    void OnAiming() 
    {
        if (transform.position != _mousePosition2D)
            transform.rotation = Quaternion.Euler(0f, 0f, GetArrowAngle());
        
        DrawLine();

        if (Input.GetMouseButtonUp(0))
        {
            this.SwitchToState(PlayerState.MOVING);
        }
    }

    void OnMoving() 
    {

    }
    void OnDisabled() 
    {

    }

    public void SwitchToState(PlayerState s)
    {
        this.state = s;
        Debug.Log("PlayerController is switching to state: " + this.state);
        switch (this.state)
        {
            case PlayerState.IDLE:
                this.SwitchToIdle();
                break;
            case PlayerState.AIMING:
                this.SwitchToAiming();
                break;
            case PlayerState.MOVING:
                this.SwitchToMoving();
                break;
            case PlayerState.DISABLED:
                this.SwitchToDisabled();
                break;
        }
    }

    void SwitchToIdle() 
    {
        if (staticStart) this.transform.position = _startPosition;
        _fader.SetColor(_levelController.levelColor);
        _fader.SwitchToState(FadeState.OUT);
        _rigidbody.velocity = Vector2.zero;
       
    }

    void SwitchToAiming() 
    {
        _fader.SwitchToState(FadeState.IN);
        _collider.enabled = false;
        if (staticStart)
        {
            this.transform.position = _startPosition;
        }
        else
        {
            this.transform.position = _mousePosition2D;
        }
        
    }

    void SwitchToMoving() 
    {
        this.ResetPlayerTrail();
		interactableColor = _levelController.levelColor;
        _levelController.SwitchToState(LevelState.WAITING);
        _collider.enabled = true;
        _lineRenderer.enabled = false;
        float shotSpeed = Mathf.Clamp(GetArrowMagnitude(), shotmin, shotmax);
        _rigidbody.AddForce(difference * shotSpeed, ForceMode2D.Impulse);
        _rigidbody.gravityScale = _gravity;
    }

    void SwitchToDisabled() { }

    Vector3 difference;
    float GetArrowAngle()
    {
        difference = (_mousePosition2D - transform.position).normalized;
        float lookAngle = Mathf.Rad2Deg * Mathf.Atan(difference.y / difference.x);
        if (difference.x < 0)
        {
            lookAngle += 180;
        }
        return lookAngle;
    }

    void DrawLine()
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, this.transform.position);

        if (Vector3.Distance(this.transform.position, _mousePosition2D) > lineMaxDist)
        {
            _lineRenderer.SetPosition(1, this.transform.position + (this.transform.right) * lineMaxDist);
        }
        else
        {
            _lineRenderer.SetPosition(1, _mousePosition2D);
        }
    }

    float GetArrowMagnitude()
    {
        float distance = Vector3.Distance(transform.position, _mousePosition2D);
        distance = distance > lineMaxDist ? lineMaxDist : distance;
        return (distance / lineMaxDist) * shotmax;
    }

    void ResetPlayerTrail()
    {
        for (int i = 0; i < _prevTrails.Count; i++)
        {
            Destroy(_prevTrails[i].gameObject);
        }

        _prevTrails.Clear();

        Destroy(_playerTrail.gameObject);
        _playerTrail = Instantiate(playerTrailPrefab, transform.position, transform.rotation) as PlayerTrail;
    }

    void SpawnTrail()
    {
        _playerTrail = Instantiate(playerTrailPrefab, this.transform.position, this.transform.rotation) as PlayerTrail;
    }

    public void SetColor(Color c)
    {
        this._fader.SetColor(c);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    public void PushNewTrail(Color c)
    {
        _prevTrails.Add(_playerTrail);
        _playerTrail.connected = false;
        this.SpawnTrail();
        this.SetColor(c);
    }

	void CheckVisible()
	{
		//CHECK IF ALREADY IN LOSE STATE
		if(
			transform.position.x > Camera.main.ViewportToWorldPoint (new Vector3(1, 0, Camera.main.nearClipPlane)).x||
			transform.position.x < Camera.main.ViewportToWorldPoint (new Vector3(0, 0, Camera.main.nearClipPlane)).x||
			transform.position.y > Camera.main.ViewportToWorldPoint (new Vector3(0, 1, Camera.main.nearClipPlane)).y||
			transform.position.y < Camera.main.ViewportToWorldPoint (new Vector3(0, 0, Camera.main.nearClipPlane)).y
			)
		{
			Debug.Log (_levelController.state);
			if(_levelController.state == LevelState.WAITING)
			{
				StartCoroutine (LeaveLevel());
				//_levelController.ResetLevel ();
			}
		}
	}

	IEnumerator LeaveLevel()
	{
		yield return new WaitForSeconds(0.75f);
		_levelController.ResetLevel ();
	}
}

public enum PlayerState
{
    IDLE, AIMING, MOVING, DISABLED
}
