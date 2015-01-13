using UnityEngine;
using System.Collections;

public class Spaceship : MonoBehaviour
{
	public float StrafeSpeed = 3f;
	public float RotateAngle = 40f;

	void FixedUpdate()
	{
		var horizontalAxis = -Input.GetAxis("Horizontal");
		// Moving ship around
		{
			var turn = horizontalAxis * StrafeSpeed;
			var movement = new Vector3(turn * Time.fixedDeltaTime, 0, 0);
			rigidbody.MovePosition(rigidbody.position + movement);
		}
		// Rotating ship
		{
			var targetAngle = horizontalAxis * RotateAngle;
			rigidbody.MoveRotation(Quaternion.Euler(
					rigidbody.rotation.eulerAngles.x,
					rigidbody.rotation.eulerAngles.y,
					targetAngle
				));
		}
	}

	const string TAG_OBSTACLE = "Obstacle";

	void OnTriggerEnter(Collider _Other)
	{
		if (_Other.gameObject.tag == TAG_OBSTACLE)
		{
			Game.Instance.Over();
		}
	}

	void Start()
	{
		var initialPosition = transform.position;
		var initialRotation = transform.rotation;
		Game.Instance.OnLaunch(delegate
		{
			transform.position = initialPosition;
			transform.rotation = initialRotation;
			gameObject.SetActive(true);
		});
		Game.Instance.OnOver(() => gameObject.SetActive(false));
	}
}
