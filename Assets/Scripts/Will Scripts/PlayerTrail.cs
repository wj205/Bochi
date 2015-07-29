using UnityEngine;
using System.Collections;

public class PlayerTrail : MonoBehaviour {

	TrailRenderer _trail;
	PlayerController _player;

	void Start()
	{
		_trail = this.GetComponent<TrailRenderer>();
		_trail.time = Mathf.Infinity;
        _player = GameObject.FindObjectOfType<PlayerController>();
		this.transform.position = _player.transform.position;
		_trail.startWidth = _player.transform.localScale.x;
		_trail.endWidth = _player.transform.localScale.x;
	}

	void Update()
	{
		HandlePosition();
	}

	void HandlePosition()
	{
		if(_player.state.Equals(PlayerState.MOVING))
		{
			this.transform.position = _player.transform.position;
		}
	}

	public void Reset()
	{
		_trail.time = 0;	
		_trail.time = Mathf.Infinity;
	}
}
