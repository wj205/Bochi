using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arrow : MonoBehaviour {

	Rigidbody2D _rigidbody;
	Renderer _renderer;
	LineRenderer _lineRenderer;
	GameController _gameController;
	PlayerTrail _playerTrail;

	public PlayerTrail playerTrailPrefab;

	public Vector3 startPoint;
	public float _gravity;
	public float shotmin;
	public float shotmax;
	public bool movingState;

	void Start()
	{
		//START IN IDLE STATE
		_rigidbody = this.GetComponent<Rigidbody2D>();
		_renderer = this.GetComponent<Renderer>();
		_lineRenderer = this.GetComponent<LineRenderer>();
		_lineRenderer.enabled = false;
		_gameController = GameObject.FindObjectOfType<GameController>().GetComponent<GameController>();
		_rigidbody.gravityScale = 0;
		startPoint = transform.position;
		SpawnTrail();
	}

	void SpawnTrail()
	{
		if(GameObject.FindObjectOfType<PlayerTrail>() == null)
		{
			_playerTrail = Instantiate (playerTrailPrefab, this.transform.position, this.transform.rotation) as PlayerTrail;
		}
	}
	
	void Update () {
		//THIS IS PROBABLY BETTER FOR PC
		HandleInput();

		//THIS MIGHT WORK BETTER ON PHONES/TABLETS
		//HandleInputAlternateSolution();	

		//I DON'T KNOW ABOUT THIS SPINNING SHIT, BUT RICH THINKS IT'S A COOL IDEA
		//HandleSpinning ();

		//JANKY WAY TO HANDLE LOSING
		CheckVisible();
	}

	void HandleInput()
	{
		if(Input.GetMouseButton (0))
		{
			//SWITCH TO AIMING STATE
			transform.rotation = Quaternion.Euler (0f, 0f, GetArrowAngle());
			DrawLine();
		}
		if(Input.GetMouseButtonUp (0))
		{
			//SWITCH TO SHOOTING STATE
			Debug.Log ("release");
			_lineRenderer.enabled = false;
			float shotSpeed = Mathf.Clamp (GetArrowMagnitude (), shotmin, shotmax);
			_rigidbody.AddForce (difference * shotSpeed, ForceMode2D.Impulse);
			_rigidbody.gravityScale = _gravity;
			ResetPlayerTrail();
			movingState = true;
		}
	}

	Vector3 difference;
	float GetArrowAngle()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 mouseWorldPos = new Vector3(mousePos.x, mousePos.y, 0f);
		difference = (mouseWorldPos - transform.position).normalized;
		float lookAngle = Mathf.Rad2Deg * Mathf.Atan (difference.y/difference.x);
		if(difference.x < 0)
		{
			lookAngle += 180;
		}
		return lookAngle;
	}

	void DrawLine()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 mouseWorldPos = new Vector3(mousePos.x, mousePos.y, 0f);
		_lineRenderer.enabled = true;
		_lineRenderer.SetPosition(0, this.transform.position);
		_lineRenderer.SetPosition (1, mouseWorldPos);
	}

	float GetArrowMagnitude()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 mouseWorldPos = new Vector3(mousePos.x, mousePos.y, 0f);
		float distance = Vector3.Distance (transform.position, mouseWorldPos);
		return distance;
	}

	void ResetPlayerTrail()
	{
		Destroy (_playerTrail.gameObject);
		_playerTrail = Instantiate (playerTrailPrefab, transform.position, transform.rotation) as PlayerTrail;
	}


	void CheckVisible()
	{
		//CHECK IF ALREADY IN LOSE STATE
		if(
		    transform.position.x > Camera.main.ViewportToWorldPoint (new Vector3(1, 0, Camera.main.nearClipPlane)).x ||
			transform.position.x < Camera.main.ViewportToWorldPoint (new Vector3(0, 0, Camera.main.nearClipPlane)).x ||
			transform.position.y > Camera.main.ViewportToWorldPoint (new Vector3(0, 1, Camera.main.nearClipPlane)).y ||
			transform.position.y < Camera.main.ViewportToWorldPoint (new Vector3(0, 0, Camera.main.nearClipPlane)).y
			)
		{
			_gameController.LoseLevel ();
		}
	}

	/*void HandleSpinning()
	{
		Debug.Log (_rigidbody.angularVelocity);
	}*/
	
	void OnCollisionEnter2D(Collision2D other)
	{
		Camera.main.GetComponent<CameraShake>().ScreenShake ();
		if(other.gameObject.tag == "Red")
		{
			_renderer.material.color = Color.red;
		}
		if(other.gameObject.tag == "Yellow")
		{
			_renderer.material.color = Color.yellow;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Target")
		{
			//Destroy (other.gameObject);
		}
		if(other.tag == "Goal")
		{
			_gameController.WinLevel ();
		}
		if(other.tag == "Death")
		{
			_gameController.LoseLevel ();
			//Destroy(this.gameObject);
		}
	}

	Vector2 exitVelocity;
	void OnTriggerStay2D(Collider2D other) 
	{
		if(other.tag == "Gravity")
		{		
			float pullForce = other.GetComponent<GravityObject>().pullForce;
			Vector3 forceDirection = other.transform.position - transform.position;
			Vector2 forceDirection2D = new Vector2(forceDirection.x, forceDirection.y);
			float distance = Vector3.Distance (transform.position, other.transform.position);
			_rigidbody.AddForce (forceDirection2D * pullForce / distance, ForceMode2D.Impulse);
			exitVelocity = _rigidbody.velocity;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Gravity")
		{
			_rigidbody.velocity = exitVelocity;
		}
	}








	// THIS IS A DIFFERENT WAY OF DEALING WITH THE SHOOTING ANGLE. FEELS MUCH WORSE ON PC, MIGHT FEEL BETTER ON PHONE/TABLET
	Vector3 initialMousePos;
	Vector3 mouseAngle;
	void HandleInputAlternateSolution()
	{
		if(Input.GetMouseButtonDown (0))
		{
			initialMousePos = Camera.main.ScreenToViewportPoint( Input.mousePosition );
		}
		if(Input.GetMouseButton (0))
		{
			float distance = Vector3.Distance (Camera.main.ScreenToViewportPoint (Input.mousePosition), initialMousePos);
			float angle = Vector3.Angle (initialMousePos, Camera.main.ScreenToViewportPoint (Input.mousePosition));
			mouseAngle = Camera.main.ScreenToViewportPoint (Input.mousePosition) - initialMousePos;
			Debug.Log (mouseAngle.magnitude * 10f);
			_lineRenderer.enabled = true;
			_lineRenderer.SetPosition (0, transform.position);
			_lineRenderer.SetPosition (1, transform.position + mouseAngle * 10f);
			HandleArrowRotation (mouseAngle.normalized);
		}
		if(Input.GetMouseButtonUp(0))
		{
			_lineRenderer.enabled = false;
			float shotSpeed = Mathf.Clamp (GetShotSpeed (), shotmin, shotmax);
			_rigidbody.AddForce (mouseAngle.normalized * shotSpeed, ForceMode2D.Impulse);
			_rigidbody.gravityScale = _gravity;
		}
	}

	void HandleArrowRotation(Vector3 mouseAngle)
	{
		float lookAngle = Mathf.Rad2Deg * Mathf.Atan (mouseAngle.y/mouseAngle.x);
		if(mouseAngle.x < 0)
		{
			lookAngle += 180;
		}
		transform.rotation = Quaternion.Euler (0f, 0f, lookAngle);
	}

	float GetShotSpeed()
	{
		return mouseAngle.magnitude * 10f;
	}
}
