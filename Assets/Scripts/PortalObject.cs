using UnityEngine;
using System.Collections;

public class PortalObject: MonoBehaviour {

	public PortalObject portalExit;
	/*public int horizontalPosModifier;
	public int verticalPosModifier;
	public int horizontalVelModifier;
	public int verticalVelModifier;*/
	public bool up;
	public bool down;
	public bool right;
	public bool left;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			PlayerController p = other.gameObject.GetComponent<PlayerController>();
			p.PushNewTrail (p.GetComponent<Renderer>().material.color);
			Vector3 hitPoint = other.transform.position;
			Vector3 difference = transform.position - hitPoint;
			Vector2 otherVelocity = other.GetComponent<Rigidbody2D>().velocity;
			if(portalExit.up)
			{
				other.transform.position = new Vector3(portalExit.transform.position.x - difference.x, portalExit.transform.position.y + portalExit.transform.localScale.y / 2f + other.transform.localScale.y / 2f, 0f);
				if(this.right || this.left)
				{
					other.GetComponent<Rigidbody2D>().velocity = new Vector2(otherVelocity.y, Mathf.Abs (otherVelocity.x));
				}else
				{
					other.GetComponent<Rigidbody2D>().velocity = new Vector2(otherVelocity.x, Mathf.Abs (otherVelocity.y));
				}
			}
			if(portalExit.down)
			{
				other.transform.position = new Vector3(portalExit.transform.position.x - difference.x, portalExit.transform.position.y - portalExit.transform.localScale.y / 2f - other.transform.localScale.y / 2f, 0f);
				if(this.right || this.left)
				{
					other.GetComponent<Rigidbody2D>().velocity = new Vector2(otherVelocity.y, -Mathf.Abs (otherVelocity.x));
				}else
				{
					other.GetComponent<Rigidbody2D>().velocity = new Vector2(otherVelocity.x, -Mathf.Abs (otherVelocity.y));
				}
			}
			if(portalExit.right)
			{
				other.transform.position = new Vector3(portalExit.transform.position.x + portalExit.transform.localScale.x / 2f + other.transform.localScale.x / 2f, portalExit.transform.position.y - difference.y, 0f);
				if(this.up || this.down)
				{
					other.GetComponent<Rigidbody2D>().velocity = new Vector2(otherVelocity.y, Mathf.Abs (otherVelocity.x));
				}else
				{
					other.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Abs (otherVelocity.x), otherVelocity.y);
				}
			}
			if(portalExit.left)
			{
				other.transform.position = new Vector3(portalExit.transform.position.x - portalExit.transform.localScale.x / 2f - other.transform.localScale.x / 2f, portalExit.transform.position.y - difference.y, 0f);
				if(this.up || this.down)
				{
					other.GetComponent<Rigidbody2D>().velocity = new Vector2(-otherVelocity.y, Mathf.Abs (otherVelocity.x));	
				}else
				{
					other.GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Abs (otherVelocity.x), otherVelocity.y);
				}
			}
			Debug.Log ("new trail");
			//Vector3 correctedDiff = new Vector3(difference.x, difference.y, 0f);
			//other.transform.position = ((portalExit.transform.position - correctedDiff) + verticalPosModifier * (transform.up * (other.transform.localScale.y /2 + transform.localScale.y / 2)));
			//Vector2 otherVelocity = other.GetComponent<Rigidbody2D>().velocity;
			//other.GetComponent<Rigidbody2D>().velocity = new Vector2 (horizontalModifier * otherVelocity.x, verticalModifier * otherVelocity.y);
		}
	}

}
