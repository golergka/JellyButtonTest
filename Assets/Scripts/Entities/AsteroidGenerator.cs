﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidGenerator : MonoBehaviour
{
	#region Singleton

	static public AsteroidGenerator Instance;

	#endregion

	#region Configuration

	public List<Transform> Spots;
	public SphereCollider AsteroidPrefab;
	public float Distance = 50f;
	public float LevelUpDistance = 300f;

	#endregion

	#region Engine methods

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		Game.Instance.OnState(delegate(Game.State _Current)
		{
			if (_Current == Game.State.Launch)
			{
				m_DistanceTraversed = 0f;
				m_DistanceGenerated = 0f;
			}
		});
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		foreach(var s in Spots)
		{
			Gizmos.DrawSphere(s.position, AsteroidPrefab.radius);
		}
	}

	void Update()
	{
		if (Game.Instance.CurrentState == Game.State.Playing)
		{
			m_DistanceTraversed += Time.deltaTime * Movement.Instance.Speed;
			if (m_DistanceTraversed - m_DistanceGenerated >= Distance)
			{
				Generate(Amount);
				m_DistanceGenerated = m_DistanceTraversed;
			}
		}
	}

	#endregion

	#region Generation

	int Amount
	{
		get
		{
			int result = 1 + Mathf.FloorToInt(m_DistanceTraversed / LevelUpDistance);
			return (result <= Spots.Count - 1) ? result : (Spots.Count - 1);
		}
	}
	
	float m_DistanceTraversed;
	float m_DistanceGenerated;

	void Generate(int _Amount)
	{
		var chosenSpots = Spots.TakeRandom(Random.Range(0, _Amount + 1));
		foreach(var spot in chosenSpots)
		{
			Instantiate(AsteroidPrefab, spot.transform.position, spot.transform.rotation);
		}
	}

	#endregion

}
