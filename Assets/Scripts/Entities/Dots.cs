using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ParticleSystem))]
public class Dots : MonoBehaviour
{
	public float EmissionFactor = 10f;

	void Update ()
	{
		particleSystem.startSpeed = Movement.Instance.Speed;
		particleSystem.emissionRate = Movement.Instance.Speed * EmissionFactor;
	}
}
