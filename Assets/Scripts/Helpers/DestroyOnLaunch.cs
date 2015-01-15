using UnityEngine;
using System.Collections.Generic;

public class DestroyOnLaunch : MonoBehaviour
{

	#region Global state

	static List<GameObject> m_ToDestroy = new List<GameObject>();
	static bool m_Init = false;

	#endregion

	#region Engine methods

	void Awake()
	{
		m_ToDestroy.Add(gameObject);
	}

	void Start()
	{
		if (!m_Init)
		{
			Game.Instance.OnState(delegate(Game.State _Current)
			{
				if (_Current == Game.State.Launch)
				{
					foreach(var g in m_ToDestroy)
					{
						Destroy(g);
					}
					m_ToDestroy.Clear();
				}
			});
			m_Init = true;
		}
	}

	#endregion
}
