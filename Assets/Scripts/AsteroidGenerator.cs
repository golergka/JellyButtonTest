using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidGenerator : MonoBehaviour
{
	public List<Transform> Spots;
	public GameObject AsteroidPrefab;
	public float Period = 1f;
	public int Amount = 1;

	void Start()
	{
		StartCoroutine(ContinueGeneration());
	}

	IEnumerator ContinueGeneration()
	{
		while(true)
		{
			Generate(Amount);
			yield return new WaitForSeconds(Period);
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
