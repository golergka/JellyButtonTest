using UnityEngine;
using System.Collections;

public class SpacheshipController : MonoBehaviour
{
	void FixedUpdate()
	{
		var turn = Input.GetAxis("Horizontal");
		transform.localPosition = new Vector3(
				transform.localPosition.x - turn * Time.fixedDeltaTime,
				transform.localPosition.y,
				transform.localPosition.z
			);
	}

	const string TAG_OBSTACLE = "Obstacle";

	void OnTriggerEnter(Collider _Other)
	{
		if (_Other.gameObject.tag == TAG_OBSTACLE)
		{
			Game.Over();
		}
	}

	void Awake()
	{
		Game.OnOver += () => gameObject.SetActive(true);
		Game.OnOver += () => gameObject.SetActive(false);
	}
}
