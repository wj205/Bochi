using UnityEngine;
using System.Collections;

public class AimingGuide : MonoBehaviour {

	Vector3 bounceVector;
	Vector3 guideAngle;
	LineRenderer _lineRenderer;

	void Start()
	{
		_lineRenderer = this.GetComponent<LineRenderer>();
	}

	public void DrawLine(GameObject player)
	{
		this.transform.position = player.transform.position;

		RaycastHit2D rayHit = Physics2D.Raycast (player.transform.position, player.transform.right, 50f);

		if(rayHit.collider != null)
		{
			Vector3 hitPoint = new Vector3(rayHit.point.x, rayHit.point.y, 0f);
			Vector3 playerPos = player.transform.position;
			float adjustment = player.transform.localScale.x / 2f;
			if(hitPoint.y > 0)
			{
				adjustment = adjustment;
			}else
			{
				adjustment = -adjustment;
			}
			bounceVector = new Vector3(hitPoint.x, hitPoint.y - adjustment, 0f);
			if(rayHit.normal.x == 0)
			{
				float xDif = bounceVector.x - playerPos.x;
				float newX = playerPos.x + xDif * 2;
				Vector3 newPos = new Vector3(newX, playerPos.y, 0f);
				Vector3 difference = (newPos - bounceVector);
				guideAngle = bounceVector + difference.normalized;
			}else if(rayHit.normal.y == 0)
			{
				float yDif = bounceVector.y - playerPos.y;
				float newY = playerPos.y + yDif * 2;
				Vector3 newPos = new Vector3(playerPos.x, newY, 0f);
				Vector3 difference = (newPos - bounceVector);
				guideAngle = bounceVector + difference.normalized;
			}
		}

		_lineRenderer.SetVertexCount (3);
		_lineRenderer.SetPosition (0, player.transform.position);
		_lineRenderer.SetPosition(1, bounceVector);
		_lineRenderer.SetPosition (2, guideAngle);

	}
}
