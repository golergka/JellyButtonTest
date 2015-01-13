using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
	void Start()
	{
		Game.Instance.DestroyOnLaunch(gameObject);
	}

	void FixedUpdate ()
	{
		transform.position = new Vector3(
				transform.position.x,
				transform.position.y,
				transform.position.z + Time.fixedDeltaTime * Game.Instance.Speed
			);
	}
}
