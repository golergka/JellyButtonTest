using UnityEngine;
using System.Collections;

public class FlyingBy : MonoBehaviour
{

	void FixedUpdate ()
	{
		transform.position = new Vector3(
				transform.position.x,
				transform.position.y,
				transform.position.z + Time.fixedDeltaTime
			);
	}
}
