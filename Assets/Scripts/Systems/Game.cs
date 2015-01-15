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
			if (m_OnState != null)
			{
				m_OnState(CurrentState);
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

	#region Callback

	public void OnState(System.Action<State> _Callback)
	{
		_Callback(CurrentState);
		m_OnState += _Callback;
	}

	event System.Action<State> m_OnState;

	#endregion
}
