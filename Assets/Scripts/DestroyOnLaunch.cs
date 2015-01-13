using UnityEngine;
using System.Collections.Generic;

public class DestroyOnLaunch : MonoBehaviour
{

	static List<GameObject> m_ToDestroy = new List<GameObject>();
	static bool m_Init;

	void Awake()
	{
		m_ToDestroy.Add(gameObject);
	}

	void Start()
	{
		if (!m_Init)
		{
			Game.Instance.OnLaunch(delegate
			{
				foreach(var g in m_ToDestroy)
				{
					Destroy(g);
				}
				m_ToDestroy.Clear();
			});
			m_Init = true;
		}
	}
}
