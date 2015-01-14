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

		if (transform.position.z >= FlyByZ)
		{
			Score.Instance.AsteroidFlownBy();
			Destroy(gameObject);
		}
	}

	public float FlyByZ = 5f;
}
