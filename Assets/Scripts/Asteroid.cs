using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
	void Start()
	{
		// TODO: Memory leak with this delegate
		Game.Instance.OnLaunch(() => Destroy(gameObject));
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
