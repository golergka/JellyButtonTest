using UnityEngine;
using System.Collections;

public class UIGameOver : MonoBehaviour
{
	#region Configuration

	public GameObject Congradulations;

	#endregion

	#region Engine methods

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

	#endregion
}
