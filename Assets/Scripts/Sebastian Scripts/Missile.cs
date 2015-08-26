using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Fader))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Missile : MonoBehaviour {

    private Rigidbody2D _rigidbody;
    private Fader _fader;
    private Collider2D _collider;
    private Vector3 _originalPosition;

	// Use this for initialization
	void Start () {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _fader = this.GetComponent<Fader>();
        _collider = this.GetComponent<Collider2D>();
        this._collider.enabled = false;
        this._originalPosition = this.transform.position;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Target"))
        {
            this._fader.SwitchToState(FadeState.OUT);
            this._rigidbody.velocity = Vector2.zero;
            this._collider.enabled = false;
        }
    }

    public void Fire(Vector3 start, float speed)
    {
        Vector2 dir = (this.transform.position - start).normalized;
        this._rigidbody.AddForce(dir * speed, ForceMode2D.Impulse);
        this._collider.enabled = true;
    }

    public void Reset()
    {
        this._collider.enabled = false;
        this.transform.position = this._originalPosition;
        this._fader.SwitchToState(FadeState.IN);
        this._rigidbody.velocity = Vector2.zero;
    }
}
