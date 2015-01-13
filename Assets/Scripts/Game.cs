using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public static Game Instance { get; private set; }

	void Awake()
	{
		Instance = this;
	}

	public void Over()
	{
		if (OnOver != null)
		{
			OnOver();
		}
	}

	public void Start()
	{
		if (OnStart != null)
		{
			OnStart();
		}
	}

	public System.Action OnOver;
	public System.Action OnStart;
}
