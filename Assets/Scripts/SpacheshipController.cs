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
			Game.Instance.Over();
		}
	}

	void Awake()
	{
		Game.Instance.OnOver += () => gameObject.SetActive(true);
		Game.Instance.OnOver += () => gameObject.SetActive(false);
	}
}
