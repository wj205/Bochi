using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GravityField : MonoBehaviour {

    private float _pullForce = 0f;
    private List<Rigidbody2D> _bodies = new List<Rigidbody2D>();

    void Update()
    {
        for (int i = 0; i < _bodies.Count; i++)
        {
            Rigidbody2D rbody = _bodies[i];
            Vector3 forceDirection = transform.position - rbody.transform.position;
            Vector2 forceDirection2D = new Vector2(forceDirection.x, forceDirection.y);
            float distance = Vector3.Distance(transform.position, rbody.transform.position);
            rbody.AddForce(forceDirection2D * _pullForce / distance, ForceMode2D.Impulse);
        }
    }

    public void SetPullForce(float f)
    {
        this._pullForce = f;
    }

    public void Reset()
    {
        this._bodies.Clear();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Body enter");
        _bodies.Add(other.gameObject.GetComponent<Rigidbody2D>());
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Body exit");
        _bodies.Remove(other.gameObject.GetComponent<Rigidbody2D>());
    }
}
