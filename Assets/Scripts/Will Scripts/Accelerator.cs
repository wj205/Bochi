﻿using UnityEngine;
using System.Collections;

public class Accelerator : MonoBehaviour {

	public float acceleratorForce = 1f;

	PlayerController _player;
	Rigidbody2D _playerRigidbody;

	void Start()
	{
		_player = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
		_playerRigidbody = _player.gameObject.GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			Accelerate ();
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			Accelerate ();
		}
	}

	void Accelerate()
	{
		if(Mathf.Abs (_playerRigidbody.velocity.x) + Mathf.Abs (_playerRigidbody.velocity.y) <= 25f)
		{
			//_playerRigidbody.AddForce (_playerRigidbody.velocity.normalized + acceleratorForce, ForceMode2D.Impulse);
			_playerRigidbody.velocity += (_playerRigidbody.velocity.normalized * acceleratorForce);
		}
	}
}
