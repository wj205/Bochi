using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Fader))]

public class PlayerController : MonoBehaviour {

    private LevelController _levelController;
    private Renderer _renderer;
    private Fader _fader;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private LineRenderer _lineRenderer;
    private PlayerTrail _playerTrail;
    public PlayerTrail playerTrailPrefab;


    private Vector3 _mousePosition = Vector3.zero;
    private Vector3 _mousePosition2D = Vector3.zero;

    public Vector3 startPoint;
    public float _gravity;
    public float shotmin;
    public float shotmax = 5f;

    public PlayerState state;

	void Start () {
        _levelController = GameObject.FindObjectOfType<LevelController>();
        _renderer = this.GetComponent<Renderer>();
        _fader = this.GetComponent<Fader>();
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _collider = this.GetComponent<Collider2D>();
        _lineRenderer = this.GetComponent<LineRenderer>();

        _playerTrail = Instantiate(playerTrailPrefab, this.transform.position, this.transform.rotation) as PlayerTrail;

        this.UpdateMousePosition();
        this.SwitchToState(PlayerState.IDLE);
	}

    void Update()
    {
        UpdateMousePosition();
        HandleStates();
    }

    void UpdateMousePosition()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition2D = new Vector3(_mousePosition.x, _mousePosition.y, 0f);
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

            if (Input.GetMouseButtonDown(0))
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
        if (Input.GetMouseButtonDown(0))
        {
            this.SwitchToState(PlayerState.IDLE);
        }
    }
    void OnDisabled() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.SwitchToState(PlayerState.IDLE);
        }
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
        _fader.SwitchToState(FadeState.OUT);
        _rigidbody.velocity = Vector2.zero;
        this.ResetPlayerTrail();
    }

    void SwitchToAiming() 
    {
        _fader.SwitchToState(FadeState.IN);
        _collider.enabled = false;
        this.transform.position = _mousePosition2D;
    }

    void SwitchToMoving() 
    {
        _levelController.SwitchToState(LevelState.WAITING);
        _collider.enabled = true;
        _lineRenderer.enabled = false;
        float shotSpeed = Mathf.Clamp(GetArrowMagnitude(), shotmin, shotmax);
        _rigidbody.AddForce(difference * shotSpeed, ForceMode2D.Impulse);
        _rigidbody.gravityScale = _gravity;
        ResetPlayerTrail();
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseWorldPos = new Vector3(mousePos.x, mousePos.y, 0f);
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, this.transform.position);
        _lineRenderer.SetPosition(1, mouseWorldPos);
    }

    float GetArrowMagnitude()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseWorldPos = new Vector3(mousePos.x, mousePos.y, 0f);
        float distance = Vector3.Distance(transform.position, mouseWorldPos);
        return distance;
    }

    void ResetPlayerTrail()
    {
        Destroy(_playerTrail.gameObject);
        _playerTrail = Instantiate(playerTrailPrefab, transform.position, transform.rotation) as PlayerTrail;
    }

    void SpawnTrail()
    {
        _playerTrail = Instantiate(playerTrailPrefab, this.transform.position, this.transform.rotation) as PlayerTrail;
    }


    void OnTriggerEnter2D(Collider2D other)
    {

    }
}

public enum PlayerState
{
    IDLE, AIMING, MOVING, DISABLED
}
