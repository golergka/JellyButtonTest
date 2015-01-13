using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
	void Start ()
	{
		Game.Instance.OnLaunch(() => gameObject.SetActive(false));
		Game.Instance.OnOver(() => gameObject.SetActive(true));
	}
}
