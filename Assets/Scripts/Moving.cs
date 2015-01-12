using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour
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
