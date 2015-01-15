using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public static Game Instance { get; private set; }

	#region Game state

	public enum State
	{
		Launch,
		Playing,
		Over
	};

	State m_CurrentState;
	public State CurrentState
	{
		get { return m_CurrentState; }
		private set
		{
			m_CurrentState = value;
			switch(m_CurrentState)
			{
				case(State.Launch):
					if (m_OnLaunch != null)
					{
						m_OnLaunch();
					}
					break;

				case(State.Playing):
					if (m_OnPlaying != null)
					{
						m_OnPlaying();
					}
					break;

				case(State.Over):
					if (m_OnOver != null)
					{
						m_OnOver();
					}
					break;
			}
		}
	}

	public void Over()
	{
		if (CurrentState == State.Playing)
		{
			CurrentState = State.Over;
		}
		else
		{
			Debug.LogError("Can't over a game that isn't playing!");
		}
	}

	public void Launch()
	{
		if (CurrentState == State.Over)
		{
			CurrentState = State.Launch;
		}
		else
		{
			Debug.LogError("Can't relaunch a game that's not over");
		}
	}

	#endregion

	#region System methods

	void Awake()
	{
		Instance = this;
		CurrentState = State.Launch;
	}

	void Update()
	{
		if (CurrentState == State.Launch && Controller.AnyKey)
		{
			CurrentState = State.Playing;
		}
	}

	#endregion

	#region Callbacks

	public void OnOver(System.Action _Callback)
	{
		if (CurrentState == State.Over)
		{
			_Callback();
		}
		m_OnOver += _Callback;
	}

	public void OnPlaying(System.Action _Callback)
	{
		if (CurrentState == State.Playing)
		{
			_Callback();
		}
		m_OnPlaying += _Callback;
	}

	public void OnLaunch(System.Action _Callback)
	{
		if (CurrentState == State.Launch)
		{
			_Callback();
		}
		m_OnLaunch += _Callback;
	}

	event System.Action m_OnOver;
	event System.Action	m_OnPlaying;
	event System.Action m_OnLaunch;

	#endregion
}
