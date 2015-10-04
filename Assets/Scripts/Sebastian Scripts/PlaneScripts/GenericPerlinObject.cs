using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GenericPerlinObject : MonoBehaviour {

    //public AudioManager aud;
    public float frequency = 1f;
    public float amplitude = 1f;
    public float noiseFactor = 5f;
    public float audioFactor = 10f;
    public float scale = 1f;
    public bool recalculateNormals = true;
    public List<int> bands = new List<int>();

    protected float _freq = 0f;
    protected float _amp = 0f;
    protected float _vol = 0f;

    protected Mesh _mesh;
    protected Vector3[] _originalVerts;
    protected Vector3[] _verts;

    protected Perlin noise;


	// Use this for initialization
	void Start () {
        this.perlinStart();

	}

    protected virtual void perlinStart()
    {
        _mesh = this.GetComponent<MeshFilter>().mesh;
        _originalVerts = _mesh.vertices;
        _verts = new Vector3[_originalVerts.Length];
        noise = new Perlin();
    }
	
	// Update is called once per frame
	void Update () {
        this.perlinUpdate();
	}

    protected virtual void perlinUpdate()
    {
        this.updateVars();
    }

    protected virtual void updateVars()
    {
        /*if (aud)
        {
            _freq = (aud.bpm / 60f) * frequency;
            _amp = amplitude;
            _vol = aud.volume;
        }
        else
        {*/
            _freq = frequency;
            _amp = amplitude;
            _vol = 1f;
        //}
    }
}
