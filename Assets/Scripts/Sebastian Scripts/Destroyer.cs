﻿using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    LevelController _levelController;
	public AudioSource death; 

	// Use this for initialization
	void Start () {
        _levelController = GameObject.FindObjectOfType<LevelController>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
			//_levelController.ResetLevel();
			if(_levelController.state == LevelState.WAITING)
			{
				_levelController.ResetLevel();
				if(death != null)
				{
					death.Play();
				}
			}
        }
    }
}
