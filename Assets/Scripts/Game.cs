using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public static Game Instance { get; private set; }

	public float LaunchSpeed	= 2f;
	public float Acceleration	= 1f;

	public float Speed { get; private set; }
	bool m_Accelerate;

	void Awake()
	{
		Instance = this;
		Speed = 0f;
		m_Accelerate = false;
	}

	void Start()
	{
		Launch();
	}

	public void Over()
	{
		Speed = 0f;
		m_Accelerate = false;
		if (OnOver != null)
		{
			OnOver();
		}
	}

	public void Launch()
	{
		Speed = LaunchSpeed;
		m_Accelerate = true;
		if (OnLaunch != null)
		{
			OnLaunch();
		}
	}

	void FixedUpdate()
	{
		if (m_Accelerate)
		{
			Speed += Acceleration * Time.fixedDeltaTime;
		}
	}

	public System.Action OnOver;
	public System.Action OnLaunch;
}
