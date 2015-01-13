using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public static Score Instance;
	void Awake()
	{
		Instance = this;
	}

	public int PerSecond = 1;
	public int PerBoostSecond = 2;

	public int Current
	{
		get { return Mathf.FloorToInt(m_CurrentFloat); }
	}
	float m_CurrentFloat;

	void Start ()
	{
		Game.Instance.OnLaunch(() => m_CurrentFloat = 0);
	}

	void Update()
	{
		if (Game.Instance.CurrentState == Game.State.Playing)
		{
			m_CurrentFloat += Time.deltaTime * (Game.Instance.Boost ? PerBoostSecond : PerSecond);
		}
	}
}
