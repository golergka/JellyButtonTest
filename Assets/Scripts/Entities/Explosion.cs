using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Light))]
public class Explosion : MonoBehaviour
{
	public AnimationCurve IntensityOverTime;

	float r_Created;

	void Awake()
	{
		r_Created = Time.time;
	}

	void Update ()
	{
		light.intensity = IntensityOverTime.Evaluate(Time.time - r_Created);
	}
}
