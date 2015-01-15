using UnityEngine;
using System.Collections;

public class UILaunch : MonoBehaviour
{
	void Start()
	{
		Game.Instance.OnState(delegate(Game.State _Current)
		{
			gameObject.SetActive(_Current == Game.State.Launch);
		});
	}
}
