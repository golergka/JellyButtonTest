using UnityEngine;
using System.Collections;

public class AsteroidCatcher : MonoBehaviour
{

	void OnTriggerEnter(Collider _Other)
	{
		if (_Other.GetComponent<Asteroid>() != null)
		{
			Destroy(_Other.gameObject);
			Score.Instance.AsteroidCought();
		}
	}

}
