using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public static Game Instance { get; private set; }

	public float LaunchSpeed	= 2f;
	public float Acceleration	= 1f;

	public float Speed { get; private set; }
	bool m_Playing;

	void Awake()
	{
		Instance = this;
		Speed = 0f;
		m_Playing = false;
	}

	void Start()
	{
		Launch();
	}

	public void Over()
	{
		Speed = 0f;
		m_Playing = false;
		if (m_OnOver != null)
		{
			m_OnOver();
		}
	}

	public void Launch()
	{
		Speed = LaunchSpeed;
		m_Playing = true;
		if (m_OnLaunch != null)
		{
			m_OnLaunch();
		}
	}

	void FixedUpdate()
	{
		if (m_Playing)
		{
			Speed += Acceleration * Time.fixedDeltaTime;
		}
	}

	public void OnOver(System.Action _Callback)
	{
		if (!m_Playing)
		{
			_Callback();
		}
		m_OnOver += _Callback;
	}

	public void OnLaunch(System.Action _Callback)
	{
		if (m_Playing)
		{
			_Callback();
		}
		m_OnLaunch += _Callback;
	}

	event System.Action m_OnOver;
	event System.Action m_OnLaunch;
}
