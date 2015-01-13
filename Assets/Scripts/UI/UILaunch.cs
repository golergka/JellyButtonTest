using UnityEngine;
using System.Collections;

public class UILaunch : MonoBehaviour
{
	void Start()
	{
		Game.Instance.OnLaunch(() => gameObject.SetActive(true));
		Game.Instance.OnPlaying(() => gameObject.SetActive(false));
	}
}
