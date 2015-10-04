using UnityEngine;
using System.Collections;

public class GravityWell : MonoBehaviour {

    private GravityField _field;
    public float pullForce = 5f;
    void Start()
    {
        _field = GetComponentInChildren<GravityField>();
        _field.SetPullForce(this.pullForce);
    }

    public void Reset()
    {
        _field.Reset();
        _field.SetPullForce(this.pullForce);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //_field.enabled = false;
        //other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
