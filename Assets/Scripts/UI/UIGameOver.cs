using UnityEngine;
using System.Collections;

public class UIGameOver : MonoBehaviour
{
	public GameObject Congradulations;
	void Start ()
	{
		Game.Instance.OnState(delegate(Game.State _Current)
		{
			if (Game.State.Over == _Current)
			{
				gameObject.SetActive(true);
				Congradulations.SetActive(Score.Instance.BrokenHigh);
			}
			else
			{
				gameObject.SetActive(false);
			}
		});
	}
}
