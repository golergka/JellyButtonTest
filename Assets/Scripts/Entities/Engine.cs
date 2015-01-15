using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class Engine : MonoBehaviour
{

	#region Configuration

	public float NormalEnginePitch = 1f;
	public float BoostEnginePitch = 2f;
	public float Attack = 1f;
	public float Release = 1f;

	#endregion

	#region Engine methods

	void Update()
	{
		var pitch = audio.pitch;
		if (Controller.Boost)
		{
			pitch += Mathf.Sign(BoostEnginePitch - NormalEnginePitch)
				* Time.deltaTime / Attack;
		}
		else
		{
			pitch += Mathf.Sign(NormalEnginePitch - BoostEnginePitch)
				* Time.deltaTime / Release;
		}
		pitch = Mathf.Clamp(pitch, NormalEnginePitch, BoostEnginePitch);
		audio.pitch = pitch;
	}

	#endregion
}
