using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{

	#region Configuration

	public float RotationSpeed = 10f;

	#endregion

	#region Engine methods

	void Start()
	{
		transform.Rotate(Vector3.up * Random.Range(0f, 360f), Space.World);
	}

	void FixedUpdate ()
	{
		transform.Rotate(Vector3.up * Time.fixedDeltaTime * RotationSpeed, Space.World);
	}

	#endregion

}
