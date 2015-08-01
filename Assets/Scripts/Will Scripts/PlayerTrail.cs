using UnityEngine;
using System.Collections;

public class PlayerTrail : MonoBehaviour {

	TrailRenderer _trail;
	PlayerController _player;
    Renderer _renderer;
    Color _color;
    public bool connected = true;
	void Start()
	{
		_trail = this.GetComponent<TrailRenderer>();
		_trail.time = Mathf.Infinity;
        _player = GameObject.FindObjectOfType<PlayerController>();
		this.transform.position = _player.transform.position;
		_trail.startWidth = _player.transform.localScale.x;
		_trail.endWidth = _player.transform.localScale.x;
        Color c = _player.GetComponent<Fader>().GetColor();
        c.a = 0.5f;
        _trail.material.color = c;
	}

	void Update()
	{
		HandlePosition();
	}

	void HandlePosition()
	{
		if(_player.state.Equals(PlayerState.MOVING) && connected)
		{
			this.transform.position = _player.transform.position + _player.transform.forward * (_player.transform.localScale.x /2f);
		}
	}

	public void Reset()
	{
		_trail.time = 0;	
		_trail.time = Mathf.Infinity;
	}
}
