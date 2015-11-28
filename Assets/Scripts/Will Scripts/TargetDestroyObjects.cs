using UnityEngine;
using System.Collections;

public class TargetDestroyObjects : MonoBehaviour {

	float timeStamp;
	Rigidbody2D _rigidbody;
	GameObject _player;

	void Start () 
	{
		//Give objs rand direction/ initial speed
		timeStamp = Time.time;
		_rigidbody = this.GetComponent<Rigidbody2D>();
		_player = GameObject.FindObjectOfType<PlayerController>().gameObject;
		Vector2 randForce = new Vector2(Random.Range (-1f,1f), Random.Range (-1f,1f));
		this.GetComponent<Rigidbody2D>().AddForce(randForce * 50f, ForceMode2D.Force);
	}

	void Update()
	{
		Vector3 difference = _player.transform.position - this.transform.position;
		Vector2 newVelocity = new Vector2(difference.x, difference.y);
		if(timeStamp + 0.5f < Time.time && timeStamp + 1f > Time.time)
		{
			_rigidbody.AddForce (newVelocity, ForceMode2D.Force);
		}else if(timeStamp + 1f < Time.time)
		{
			_rigidbody.velocity = newVelocity * ((Time.time - timeStamp));
		}
	}

}
