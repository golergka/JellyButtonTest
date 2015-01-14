using UnityEngine;
using System.Collections;

public class UIGameOver : MonoBehaviour
{
	public GameObject Congradulations;
	void Start ()
	{
		Game.Instance.OnLaunch(() => gameObject.SetActive(false));
		Game.Instance.OnOver(delegate
		{
			gameObject.SetActive(true);
			Congradulations.SetActive(Score.Instance.BrokenHigh);
		});
	}
}
