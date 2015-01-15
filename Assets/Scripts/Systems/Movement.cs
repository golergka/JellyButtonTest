using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	#region Singleton

	public static Movement Instance { get; private set; }

	#endregion

	#region Configuration

	public float StartSpeed			= 6f;
	public float Acceleration		= 1.5f;
	public float BoostMultiplier	= 2f;

	#endregion

	#region Engine methods

	void FixedUpdate()
	{
		if (Game.Instance.CurrentState == Game.State.Playing)
		{
			m_BaseSpeed += Acceleration * Time.fixedDeltaTime;
		}
	}

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		Game.Instance.OnState(delegate(Game.State _Current)
		{
			switch(_Current)
			{
				case(Game.State.Launch):
					m_BaseSpeed = 0f;
					break;
				case(Game.State.Playing):
					m_BaseSpeed = StartSpeed;
					break;
				case(Game.State.Over):
					m_BaseSpeed = 0f;
					break;
			}
		});
	}

	#endregion

	#region Speed 

	public float Speed
	{
		get
		{
			return m_BaseSpeed * (Controller.Boost ? BoostMultiplier : 1f);
		}
	}

	float m_BaseSpeed;

	#endregion
}
