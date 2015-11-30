using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	public GameObject[] asteroids;

	void Update()
	{
		SpawnAsteroids();
	}


	float spawnTimer = 0f;
	float spawnMax = 3f;
	void SpawnAsteroids()
	{
		if(spawnTimer < spawnMax)
		{
			spawnTimer += Time.deltaTime;
		}else
		{
			SpawnAsteroid();
			GetSpawnMax();
			spawnTimer = 0f;
		}
	}

	void GetSpawnMax()
	{
		spawnMax = Random.Range (3f, 8f);
	}

	void SpawnAsteroid()
	{
		float rand = Random.Range (0f, 1f);
		float posY = Random.Range (-10f, 10f);
		float posX = 0f;
		if(rand > 0.5f)
		{
			posX = -15f;
		}else
		{
			posX = 15f;
		}
		Vector3 asteroidPos = new Vector3(posX, posY, 5f);
		GameObject newAsteroid = Instantiate (GetRandomAsteroid(), asteroidPos, Quaternion.identity) as GameObject;
	}

	GameObject GetRandomAsteroid()
	{
		int asteroidNum = asteroids.Length;
		int randAsteroid = Random.Range (0, asteroidNum);
		return asteroids[randAsteroid];
	}
}
