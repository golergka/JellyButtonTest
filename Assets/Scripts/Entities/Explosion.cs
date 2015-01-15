using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Light))]
public class Explosion : MonoBehaviour
{

	#region Configuration

	public AnimationCurve IntensityOverTime;

	#endregion

	#region Initial parameters

	float r_Created;

	#endregion

	#region Engine methods

	void Awake()
	{
		r_Created = Time.time;
	}

	void Update ()
	{
		light.intensity = IntensityOverTime.Evaluate(Time.time - r_Created);
	}

	#endregion
}
