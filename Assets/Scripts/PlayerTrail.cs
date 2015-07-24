using UnityEngine;
using System.Collections;

public class PlayerTrail : MonoBehaviour {

	LineRenderer _lineRenderer;
	public Vector3[] trailPoints;

	void Start()
	{
		_lineRenderer = this.GetComponent<LineRenderer>();
		_lineRenderer.enabled = false;
	}

	public void DrawTrail()
	{
		_lineRenderer.enabled = true;
		for (int i = 0; i < trailPoints.Length; i++)
		{
			_lineRenderer.SetPosition(i, trailPoints[i]);
		}
	}

}
