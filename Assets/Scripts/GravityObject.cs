using UnityEngine;
using System.Collections;

public class GravityObject : MonoBehaviour {

	public float pullRadius;
	public float pullForce;

	void Start()
	{
		transform.localScale = new Vector3(pullRadius, pullRadius, transform.localScale.z);
	}

	/*void FixedUpdate()
	{
		Collider2D[] colliders = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), pullRadius);
		//foreach(Collider2D collider in Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), pullRadius))
		for(int i = 0; i < colliders.Length; i++)
		{
			Vector3 forceDirection = transform.position - colliders[i].transform.position;
			Vector2 forceDirection2D = new Vector2(forceDirection.x, forceDirection.y);

			colliders[i].GetComponent<Rigidbody2D>().AddForce (forceDirection2D.normalized * pullForce * Time.deltaTime);
		}
	}*/

	/*void OnTriggerStay2D(Collider2D other)
	{
		Debug.Log ("there's something inside of me");
		Vector3 forceDirection = transform.position - other.transform.position;
		Vector2 forceDirection2D = new Vector2(forceDirection.x, forceDirection.y);
		other.GetComponent<Rigidbody2D>().AddForce (forceDirection2D.normalized * pullForce * Time.deltaTime);
	}*/
}
