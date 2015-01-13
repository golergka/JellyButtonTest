using UnityEngine;
using System.Collections;

public static class Game
{
	public static void Over()
	{
		if (OnOver != null)
		{
			OnOver();
		}
	}

	public static void Start()
	{
		if (OnStart != null)
		{
			OnStart();
		}
	}

	public static System.Action OnOver;
	public static System.Action OnStart;
}
