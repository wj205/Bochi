using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	Arrow _player;
	PlayerTrail _playerTrail;

	void Start()
	{
		Time.timeScale = 1;
		_player = GameObject.FindObjectOfType<Arrow>().GetComponent<Arrow>();
		_playerTrail = GameObject.FindObjectOfType<PlayerTrail>().GetComponent<PlayerTrail>();
	}
	
	void Update () {
		if(Input.GetKeyDown (KeyCode.R))
		{
			Restart ();
		}
	}

	public void Restart () 
	{
		//Application.LoadLevel (Application.loadedLevel); 
		_player.transform.position = _player.startPoint;
		_player.transform.rotation = Quaternion.Euler (Vector3.zero);
		_player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		_playerTrail.DrawTrail ();
	}

	public void WinLevel () 
	{
		//SWITCH TO GAME WON STATE
		Debug.Log ("You Won!");

		//DO THIS ON SOME INPUT IN THE WIN GAME STATE
		if(Application.loadedLevel < Application.levelCount - 1)
		{
			Application.LoadLevel (Application.loadedLevel + 1);
		}else
		{
			Application.LoadLevel (0);
		}
	}
	
	public void LoseLevel ()
	{
		//SWITCH TO GAME LOSE STATE
		Restart ();
	}
}