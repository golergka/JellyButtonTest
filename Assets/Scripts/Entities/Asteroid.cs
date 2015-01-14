using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
	void FixedUpdate ()
	{
		transform.position = new Vector3(
				transform.position.x,
				transform.position.y,
				transform.position.z + Time.fixedDeltaTime * Game.Instance.Speed
			);

		transform.Rotate(Vector3.up * Time.fixedDeltaTime * RotationSpeed, Space.World);
		if (transform.position.z >= FlyByZ)
		{
			Score.Instance.AsteroidFlownBy();
			Destroy(gameObject);
		}
	}

	public float RotationSpeed = 10f;
	public float FlyByZ = 5f;
}
