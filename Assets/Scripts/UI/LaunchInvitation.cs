using UnityEngine;
using System.Collections;

public class LaunchInvitation : MonoBehaviour
{
	void Start()
	{
		Game.Instance.OnLaunch(() => gameObject.SetActive(true));
		Game.Instance.OnPlaying(() => gameObject.SetActive(false));
	}
}
