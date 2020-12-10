using UnityEngine;
using System.Collections;

public class SolarSystem : MonoBehaviour 
{
	public Transform theStar;
	public float globalSpeed = 1; // Overall Speed
	public Planets[] planets;

	void Start()
	{
		/*GetDistance*/
		for(int j = 0; j < planets.Length; j++)
		{
			planets[j].distance = (planets[j].target.position - theStar.position).magnitude;
			for(int i = 0; i < planets[j].satellite.Length; i++)
				planets[j].satellite[i].distance = (planets[j].satellite[i].target.position - planets[j].target.position).magnitude;
		}
	}

	void FixedUpdate()
	{
		/*Move*/
		for(int j = 0; j < planets.Length; j++)
		{
			planets[j].target.position = GetPositon(theStar.position, planets[j].distance, planets[j].currentAng, planets[j].offsetSin, planets[j].offsetCos);
			planets[j].target.Rotate(Vector3.up * planets[j].rotationSpeed * globalSpeed);
			planets[j].currentAng += planets[j].speed * Time.deltaTime * globalSpeed;

			for(int i = 0; i < planets[j].satellite.Length; i++)
			{
				planets[j].satellite[i].target.position = GetPositon(planets[j].target.position, planets[j].satellite[i].distance, planets[j].satellite[i].currentAng, planets[j].satellite[i].offsetSin, planets[j].satellite[i].offsetCos);
				planets[j].satellite[i].target.Rotate(Vector3.up * planets[j].satellite[i].rotationSpeed * globalSpeed);
				planets[j].satellite[i].currentAng += planets[j].satellite[i].speed * Time.deltaTime * globalSpeed;
			}
		}
	}

	Vector3 GetPositon(Vector3 around, float dist, float angle, float sin, float cos)
	{
		around.x += Mathf.Sin(angle) * dist * sin;
		around.z += Mathf.Cos(angle) * dist * cos;
		return around;
	}
}

[System.Serializable]
public class Planets
{
	public Transform target;
	public float speed = 0.5f;
	public float rotationSpeed = 1;
	public float offsetSin = 1, offsetCos = 1; // If != 1, then there will be an oval offset
	[HideInInspector] public float distance, currentAng;
	public Satellite[] satellite;
}


[System.Serializable]
public class Satellite
{
	public Transform target;
	public float speed = 0.05f;
	public float rotationSpeed = 1;
	public float offsetSin = 1, offsetCos = 1;
	[HideInInspector] public float distance, currentAng;
}