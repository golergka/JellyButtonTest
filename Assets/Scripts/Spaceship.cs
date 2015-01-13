using UnityEngine;
using System.Collections;

public class Spaceship : MonoBehaviour
{
	public float StrafeSpeed = 3f;
	public float RotateAngle = 40f;

	void FixedUpdate()
	{
		var horizontalAxis = -Input.GetAxis("Horizontal");
		var turn = horizontalAxis * StrafeSpeed;
		transform.localPosition = new Vector3(
				transform.localPosition.x + turn * Time.fixedDeltaTime,
				transform.localPosition.y,
				transform.localPosition.z
			);
		transform.eulerAngles = new Vector3(
				transform.eulerAngles.x,
				transform.eulerAngles.y,
				horizontalAxis * RotateAngle
			);
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
		Game.Instance.OnLaunch(() => gameObject.SetActive(true));
		Game.Instance.OnOver(() => gameObject.SetActive(false));
	}
}
