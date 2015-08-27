using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

    public float rotateSpeed = 0f;
    public float angle = 0f;
    public bool slerp = false;

    private Quaternion _goalRotation = Quaternion.identity;
    private float _goalAngle;

	// Use this for initialization
	void Start () {
        _goalAngle = this.transform.rotation.eulerAngles.z + angle;
        _goalRotation = Quaternion.AngleAxis(_goalAngle, Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(this.transform.rotation.eulerAngles.z - _goalAngle) < 0.1f)
        {
            _goalAngle = (_goalAngle + angle) % 360f;
            _goalRotation = Quaternion.AngleAxis(_goalAngle,Vector3.forward);
        }
        else
        {
            if (slerp)
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, _goalRotation, this.rotateSpeed * Time.deltaTime);
            }
            else
            {
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, _goalRotation, this.rotateSpeed * Time.deltaTime);
            }
            
        }

	}
}
