using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    LevelController _levelController;

	// Use this for initialization
	void Start () {
        _levelController = GameObject.FindObjectOfType<LevelController>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
			if(_levelController.state == LevelState.WAITING)
			{
            	_levelController.ResetLevel();
			}
        }
    }
}
