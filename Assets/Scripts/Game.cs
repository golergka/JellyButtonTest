using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public static Game Instance { get; private set; }

	#region Configuration

	public float StartSpeed		= 2f;
	public float Acceleration	= 1f;
	public float BoostMultiplier = 5f;

	#endregion

	#region Speed

	public float Speed
	{
		get
		{
			return m_BaseSpeed * (Input.GetButton("Boost") ? BoostMultiplier : 1f);
		}
	}

	float m_BaseSpeed;

	#endregion

	#region Game state

	enum State
	{
		Launch,
		Playing,
		Over
	};

	State m_CurrentState;
	State CurrentState
	{
		get { return m_CurrentState; }
		set
		{
			m_CurrentState = value;
			switch(m_CurrentState)
			{
				case(State.Launch):
					m_BaseSpeed = 0f;
					if (m_OnLaunch != null)
					{
						m_OnLaunch();
					}
					break;

				case(State.Playing):
					m_BaseSpeed = StartSpeed;
					if (m_OnPlaying != null)
					{
						m_OnPlaying();
					}
					break;

				case(State.Over):
					m_BaseSpeed = 0f;
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
		if (CurrentState == State.Launch && Input.GetAxis("Horizontal") != 0f)
		{
			CurrentState = State.Playing;
		}
	}

	#endregion

	#region Callbacks

	void FixedUpdate()
	{
		if (CurrentState == State.Playing)
		{
			m_BaseSpeed += Acceleration * Time.fixedDeltaTime;
		}
	}

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
