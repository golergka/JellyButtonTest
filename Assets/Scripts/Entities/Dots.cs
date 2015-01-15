using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ParticleSystem))]
public class Dots : MonoBehaviour
{
	#region Configuration

	public float EmissionFactor = 10f;

	#endregion

	#region Engine methods

	void Update ()
	{
		particleSystem.startSpeed = Movement.Instance.Speed;
		particleSystem.emissionRate = Movement.Instance.Speed * EmissionFactor;
	}

	#endregion
}
