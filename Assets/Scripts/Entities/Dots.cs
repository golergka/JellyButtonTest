using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ParticleSystem))]
public class Dots : MonoBehaviour
{
	public float EmissionFactor = 10f;

	void Update ()
	{
		particleSystem.startSpeed = Game.Instance.Speed;
		particleSystem.emissionRate = Game.Instance.Speed * EmissionFactor;
	}
}
