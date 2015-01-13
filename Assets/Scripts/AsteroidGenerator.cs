using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidGenerator : MonoBehaviour
{
	public List<Transform> Spots;
	public GameObject AsteroidPrefab;
	public float Distance = 50f;
	public int Amount = 1;

	void Start()
	{
		Game.Instance.OnLaunch += () => StartGeneration();
		Game.Instance.OnOver += () => StopGeneration();
	}

	IEnumerator m_GenerationRoutine;
	
	void StartGeneration()
	{
		if (m_GenerationRoutine == null)
		{
			m_GenerationRoutine = ContinueGeneration();
			StartCoroutine(m_GenerationRoutine);
		}
	}

	void StopGeneration()
	{
		if (m_GenerationRoutine != null)
		{
			StopCoroutine(m_GenerationRoutine);
			m_GenerationRoutine = null;
		}
	}

	IEnumerator ContinueGeneration()
	{
		while(true)
		{
			Generate(Amount);
			yield return new WaitForSeconds(Distance / Game.Instance.Speed);
		}
	}
	
	void Generate(int _Amount)
	{
		var chosenSpots = Spots.TakeRandom(_Amount);
		foreach(var spot in chosenSpots)
		{
			Instantiate(AsteroidPrefab, spot.transform.position, spot.transform.rotation);
		}
	}
}
