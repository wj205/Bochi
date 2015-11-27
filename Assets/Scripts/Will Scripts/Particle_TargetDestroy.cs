using UnityEngine;
using System.Collections;

public class Particle_TargetDestroy : MonoBehaviour {

	/*ParticleEmitter _emitter;
	Particle[] _particles;
	GameObject _player;
	float timeStamp;

	void Start()
	{
		_emitter = this.GetComponent<ParticleEmitter>();
		_player = GameObject.FindObjectOfType<PlayerController>().gameObject;
		timeStamp = Time.time;
		SpawnParticles();
	}

	void Update()
	{
		_particles = _emitter.particles;
		if(timeStamp + 0.75f < Time.time)
		{
			for(int i = 0; i < _particles.Length; i++)
			{
				Debug.Log ("doing things");
				Vector3 difference = _player.transform.position - _particles[i].position;
				_particles[i].velocity = difference.normalized;
			}
		}
		_emitter.particles = _particles;		
	}

	void SpawnParticles()
	{
		_emitter.Emit();
	}*/

	ParticleSystem _particleSystem;
	ParticleSystem.Particle[] _particles;
	GameObject _player;
	float timeStamp;

	void Start()
	{
		_particleSystem = this.GetComponent<ParticleSystem>();
		_player = GameObject.FindObjectOfType<PlayerController>().gameObject;
		timeStamp = Time.time;
	}

	void Update()
	{
		int pCount = _particleSystem.particleCount;
		_particles = new ParticleSystem.Particle[pCount];
		_particleSystem.GetParticles (_particles);
		if(timeStamp + 0.75f < Time.time)
		{
			for(int i = 0; i < _particles.Length; i++)
			{
				Debug.Log ("doing things");
				Vector3 difference = _player.transform.position - _particles[i].position;
				_particles[i].velocity = difference.normalized;
			}
		}
		_particleSystem.SetParticles (_particles, _particles.Length);
	}
}
