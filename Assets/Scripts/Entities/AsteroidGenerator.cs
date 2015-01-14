﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidGenerator : MonoBehaviour
{
	public List<Transform> Spots;
	public SphereCollider AsteroidPrefab;
	public float Distance = 50f;
	public int Amount = 1;
	
	float m_DistanceTraversed;
	float m_DistanceGenerated;

	void Start()
	{
		Game.Instance.OnLaunch(delegate {
			m_DistanceTraversed = 0f;
			m_DistanceGenerated = 0f;
		});
	}

	void Generate(int _Amount)
	{
		var chosenSpots = Spots.TakeRandom(_Amount);
		foreach(var spot in chosenSpots)
		{
			Instantiate(AsteroidPrefab, spot.transform.position, spot.transform.rotation);
		}
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
			m_DistanceTraversed += Time.deltaTime * Game.Instance.Speed;
			if (m_DistanceTraversed - m_DistanceGenerated >= Distance)
			{
				Generate(Amount);
				m_DistanceGenerated = m_DistanceTraversed;
			}
		}
	}
}
