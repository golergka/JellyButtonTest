using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public static Game Instance { get; private set; }

	#region Configuration

	public float StartSpeed	= 2f;
	public float Acceleration	= 1f;

	#endregion

	public float Speed { get; private set; }

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
					Speed = 0f;
					if (m_OnLaunch != null)
					{
						m_OnLaunch();
					}
					break;

				case(State.Playing):
					Speed = StartSpeed;
					if (m_OnPlaying != null)
					{
						m_OnPlaying();
					}
					break;

				case(State.Over):
					Speed = 0f;
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
		CurrentState = State.Over;
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
			Speed += Acceleration * Time.fixedDeltaTime;
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
