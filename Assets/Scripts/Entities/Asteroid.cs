using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
	#region Configuration

	public float FlyByZ = 5f;
	
	#endregion

	#region Engine methods

	void FixedUpdate ()
	{
		transform.position = new Vector3(
				transform.position.x,
				transform.position.y,
				transform.position.z + Time.fixedDeltaTime * Movement.Instance.Speed
			);

		if (transform.position.z >= FlyByZ)
		{
			Score.Instance.AsteroidFlownBy();
			Destroy(gameObject);
		}
	}

	#endregion

}
