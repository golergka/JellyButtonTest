using UnityEngine;
using System.Collections;

public static class Controller
{
	static public float Steering
	{
		get { return Input.GetAxis("Horizontal"); }
	}

	static public bool Boost
	{
		get { return Input.GetButton("Boost"); }
	}

	static public bool AnyKey
	{
		get { return Input.anyKey; }
	}
}
